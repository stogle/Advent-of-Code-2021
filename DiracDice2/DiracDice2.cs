using static System.Formats.Asn1.AsnWriter;

namespace AdventOfCode2021.DiracDice;

public class DiracDice2
{
    private const short WinningScore = 21;

    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var diracDice = new DiracDice2(reader);
        long result = diracDice.GetMaxTotalUniversesWherePlayerWins();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public DiracDice2(TextReader reader)
    {
        _reader = reader;
    }

    private static readonly (int Roll, long Count)[] Outcomes = { (3, 1), (4, 3), (5, 6), (6, 7), (7, 6), (8, 3), (9, 1) };

    public long GetMaxTotalUniversesWherePlayerWins()
    {
        Stack<(int Position1, int Position2, int Score1, int Score2, bool IsPlayer2Turn, long Count)> states = new();
        states.Push((int.Parse(_reader.ReadLine()!.Substring(28)), int.Parse(_reader.ReadLine()!.Substring(28)), 0, 0, false, 1L));

        long player1Wins = 0;
        long player2Wins = 0;
        while (states.Any())
        {
            (int position1, int position2, int score1, int score2, bool isPlayer2Turn, long count) = states.Pop();

            if (score1 >= WinningScore)
            {
                player1Wins += count;
            }
            else if (score2 >= WinningScore)
            {
                player2Wins += count;
            }
            else
            {
                foreach (var outcome in Outcomes)
                {
                    int newPosition = ((isPlayer2Turn ? position2 : position1) + outcome.Roll - 1) % 10 + 1;
                    var newState = isPlayer2Turn
                        ? (position1, newPosition, score1, score2 + newPosition, false, count * outcome.Count)
                        : (newPosition, position2, score1 + newPosition, score2, true, count * outcome.Count);
                    states.Push(newState);
                }
            }
        }

        return Math.Max(player1Wins, player2Wins);
    }
}
