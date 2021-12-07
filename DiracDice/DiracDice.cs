namespace AdventOfCode2021.DiracDice;

public class DiracDice
{
    private const int WinningScore = 1000;
    private const int Player1 = 0;
    private const int Player2 = 1;

    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var diracDice = new DiracDice(reader);
        int result = diracDice.GetLosingScoreTimesDieRolls();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public DiracDice(TextReader reader)
    {
        _reader = reader;
    }

    public int GetLosingScoreTimesDieRolls()
    {
        int[] positions =
        {
            int.Parse(_reader.ReadLine()!.Substring(28)),
            int.Parse(_reader.ReadLine()!.Substring(28))
        };
        int[] scores = new int[2];

        var die = new DeterministicDie();
        int player = Player1;
        while (scores[Player1] < WinningScore && scores[Player2] < WinningScore)
        {
            int total = die.Roll() + die.Roll() + die.Roll();
            positions[player] = (positions[player] + total - 1) % 10 + 1;
            scores[player] += positions[player];
            player = player == Player1 ? Player2 : Player1;
        }

        return scores.Min() * die.Rolls;
    }

    private class DeterministicDie
    {
        private int _nextValue = 1;
        
        public int Rolls { get; private set; }

        public int Roll()
        {
            int result = _nextValue;
            _nextValue = _nextValue % 100 + 1;
            Rolls++;
            return result;
        }
    }
}
