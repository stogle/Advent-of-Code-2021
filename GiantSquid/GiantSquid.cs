namespace AdventOfCode2021.GiantSquid;

public class GiantSquid
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var giantSquid = new GiantSquid(reader);
        int? score = giantSquid.GetScore();
        Console.WriteLine(score);
    }

    private readonly TextReader _reader;

    public GiantSquid(TextReader reader)
    {
        _reader = reader;
    }

    public int? GetScore()
    {
        List<int> numbers = _reader.ReadLine()!.Split(',').Select(int.Parse).ToList();
        List<(int Number, bool Marked)[][]> boards = new List<(int, bool)[][]>();
        while (_reader.ReadLine() != null)
        {
            (int Number, bool Marked)[][] board = new (int, bool)[5][];
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = _reader.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => (int.Parse(s), false)).ToArray();
            }
            boards.Add(board);
        }

        foreach (int number in numbers)
        {
            foreach ((int Number, bool Marked)[][] board in boards)
            {
                for (int i = 0; i < board.Length; i++)
                {
                    for (int j = 0; j < board[i].Length; j++)
                    {
                        if (board[i][j].Number == number)
                        {
                            board[i][j].Marked = true;
                        }
                    }
                }

                if (board.Any(row => row.All(square => square.Marked)) ||
                    board.All(row => row[0].Marked) ||
                    board.All(row => row[1].Marked) ||
                    board.All(row => row[2].Marked) ||
                    board.All(row => row[3].Marked) ||
                    board.All(row => row[4].Marked))
                {
                    return number * board
                        .SelectMany(row => row)
                        .Where(square => !square.Marked)
                        .Select(square => square.Number)
                        .Sum();
                }
            }
        }

        return null;
    }
}
