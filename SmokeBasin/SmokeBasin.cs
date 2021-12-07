namespace AdventOfCode2021.SmokeBasin;

public class SmokeBasin
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var smokeBasin = new SmokeBasin(reader);
        int result = smokeBasin.GetTotalRiskLevels();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public SmokeBasin(TextReader reader)
    {
        _reader = reader;
    }

    public int GetTotalRiskLevels()
    {
        var heightmap = new List<int[]>();
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            int[] heights = line.Select(c => int.Parse(c.ToString())).ToArray();
            heightmap.Add(heights);
        }

        int result = 0;
        for (int i = 0, n = heightmap.Count - 1; i <= n; i++)
        {
            for (int j = 0, m = heightmap[i].Length - 1; j <= m; j++)
            {
                int height = heightmap[i][j];
                if ((i <= 0 || height < heightmap[i - 1][j]) &&
                    (i >= n || height < heightmap[i + 1][j]) &&
                    (j <= 0 || height < heightmap[i][j - 1]) &&
                    (j >= m || height < heightmap[i][j + 1]))
                {
                    result += 1 + height;
                }
            }
        }

        return result;
    }
}
