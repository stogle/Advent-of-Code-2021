namespace AdventOfCode2021.SeaCucumber;

public class SeaCucumber
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var seaCucumber = new SeaCucumber(reader);
        int result = seaCucumber.GetMinimumStepWithNoMoves();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public SeaCucumber(TextReader reader)
    {
        _reader = reader;
    }

    public int GetMinimumStepWithNoMoves()
    {
        var region = new List<char[]>();
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            region.Add(line.Select(c => c).ToArray());
        }

        int step = 0;
        bool isMovement;
        do
        {
            isMovement = false;

            // East-facing
            var nextRegion = region.Select(row => row.Select(c => c == '>' ? '.' : c).ToArray()).ToList();
            for (int i = 0; i < region.Count; i++)
            {
                for (int j = 0, n = region[i].Length; j < n; j++)
                {
                    if (region[i][j] == '>')
                    {
                        if (region[i][(j + 1) % n] == '.')
                        {
                            isMovement = true;
                            nextRegion[i][(j + 1) % n] = '>';
                        }
                        else
                        {
                            nextRegion[i][j] = '>';
                        }
                    }
                }
            }
            region = nextRegion;

            // South-facing
            nextRegion = region.Select(row => row.Select(c => c == 'v' ? '.' : c).ToArray()).ToList();
            for (int j = 0; j < region[0].Length; j++)
            {
                for (int i = 0, n = region.Count; i < n; i++)
                {
                    if (region[i][j] == 'v')
                    {
                        if (region[(i + 1) % n][j] == '.')
                        {
                            isMovement = true;
                            nextRegion[(i + 1) % n][j] = 'v';
                        }
                        else
                        {
                            nextRegion[i][j] = 'v';
                        }
                    }
                }
            }

            region = nextRegion;
            step++;
        }
        while (isMovement);

        return step;
    }
}
