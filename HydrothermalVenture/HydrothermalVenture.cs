namespace AdventOfCode2021.HydrothermalVenture;

public class HydrothermalVenture
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var hydrothermalVenture = new HydrothermalVenture(reader);
        int result = hydrothermalVenture.GetDangerousPointCount();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public HydrothermalVenture(TextReader reader)
    {
        _reader = reader;
    }

    public int GetDangerousPointCount()
    {
        string? line;
        var ventPoints = new Dictionary<(int X, int Y), int>();
        while ((line = _reader.ReadLine()) != null)
        {
            string[] endpoints = line.Split(" -> ");
            string[] start = endpoints[0].Split(",");
            string[] end = endpoints[1].Split(",");
            int x1 = int.Parse(start[0]);
            int y1 = int.Parse(start[1]);
            int x2 = int.Parse(end[0]);
            int y2 = int.Parse(end[1]);
            if (x1 == x2 || y1 == y2)
            {
                for (int x = Math.Min(x1, x2), n = Math.Max(x1, x2); x <= n; x++)
                {
                    for (int y = Math.Min(y1, y2), m = Math.Max(y1, y2); y <= m; y++)
                    {
                        ventPoints.TryGetValue((x, y), out int count);
                        ventPoints[(x, y)] = count + 1;
                    }
                }
            }
        }

        return ventPoints.Count(entry => entry.Value >= 2);
    }
}
