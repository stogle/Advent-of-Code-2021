namespace AdventOfCode2021.DumboOctopus;

public class DumboOctopus2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var dumboOctopus = new DumboOctopus2(reader);
        int result = dumboOctopus.GetFirstStepWhereAllFlash();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public DumboOctopus2(TextReader reader)
    {
        _reader = reader;
    }

    public int GetFirstStepWhereAllFlash()
    {
        List<(int Energy, bool Flashed)[]> octopuses = new List<(int, bool)[]>();
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            octopuses.Add(line.Select(c => (int.Parse(c.ToString()), false)).ToArray());
        }

        int result = 0;
        do
        {
            result++;

            foreach (var row in octopuses)
            {
                for (int j = 0; j < row.Length; j++)
                {
                    if (row[j].Flashed)
                    {
                        row[j].Energy = 0;
                        row[j].Flashed = false;
                    }
                    row[j].Energy++;
                }
            }

            bool flashed;
            do
            {
                flashed = false;
                for (int i = 0, n = octopuses.Count - 1; i <= n; i++)
                {
                    for (int j = 0, m = octopuses[i].Length - 1; j <= m; j++)
                    {
                        if (octopuses[i][j].Energy > 9 && !octopuses[i][j].Flashed)
                        {
                            octopuses[i][j].Flashed = true;
                            if (i > 0)
                            {
                                if (j > 0)
                                {
                                    octopuses[i - 1][j - 1].Energy++;
                                }
                                octopuses[i - 1][j].Energy++;
                                if (j < m)
                                {
                                    octopuses[i - 1][j + 1].Energy++;
                                }
                            }
                            if (j > 0)
                            {
                                octopuses[i][j - 1].Energy++;
                            }
                            if (j < m)
                            {
                                octopuses[i][j + 1].Energy++;
                            }
                            if (i < n)
                            {
                                if (j > 0)
                                {
                                    octopuses[i + 1][j - 1].Energy++;
                                }
                                octopuses[i + 1][j].Energy++;
                                if (j < m)
                                {
                                    octopuses[i + 1][j + 1].Energy++;
                                }
                            }

                            flashed = true;
                        }
                    }
                }
            }
            while (flashed);
        }
        while (octopuses.SelectMany(row => row).Any(o => !o.Flashed));

        return result;
    }
}
