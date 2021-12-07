namespace AdventOfCode2021.SmokeBasin;

public class SmokeBasin2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var smokeBasin = new SmokeBasin2(reader);
        int result = smokeBasin.GetLargestBasinsProduct();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public SmokeBasin2(TextReader reader)
    {
        _reader = reader;
    }

    public int GetLargestBasinsProduct()
    {
        var heightmap = new List<int[]>();
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            int[] heights = line.Select(c => int.Parse(c.ToString())).ToArray();
            heightmap.Add(heights);
        }

        var basins = new Dictionary<(int, int), int>();
        for (int i = 0, n = heightmap.Count - 1; i <= n; i++)
        {
            for (int j = 0, m = heightmap[i].Length - 1; j <= m; j++)
            {
                int height = heightmap[i][j];
                if (height == 9)
                {
                    continue;
                }

                int x = i, y = j;
                while (true)
                {
                    if (x > 0 && heightmap[x - 1][y] < height)
                    {
                        x--;
                    }
                    else if (x < n && heightmap[x + 1][y] < height)
                    {
                        x++;
                    }
                    else if (y > 0 && heightmap[x][y - 1] < height)
                    {
                        y--;
                    }
                    else if (y < m && heightmap[x][y + 1] < height)
                    {
                        y++;
                    }
                    else
                    {
                        break;
                    }

                    height = heightmap[x][y];
                }

                if (!basins.TryGetValue((x, y), out int basinSize))
                {
                    basinSize = 0;
                }
                basins[(x, y)] = basinSize + 1;
            }
        }

        return basins.Values
            .OrderByDescending(size => size)
            .Take(3)
            .Aggregate(1, (int product, int size) => product * size);
    }
}
