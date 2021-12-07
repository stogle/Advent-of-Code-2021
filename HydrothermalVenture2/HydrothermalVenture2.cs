namespace AdventOfCode2021.HydrothermalVenture;

public class HydrothermalVenture2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var hydrothermalVenture = new HydrothermalVenture2(reader);
        int result = hydrothermalVenture.GetDangerousPointCount();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public HydrothermalVenture2(TextReader reader)
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
            int length = Math.Max(Math.Abs(x2 - x1), Math.Abs(y2 - y1));
            int xIncrement = Math.Sign(x2 - x1);
            int yIncrement = Math.Sign(y2 - y1);
            for (int i = 0, x = x1, y = y1; i <= length; i++, x += xIncrement, y += yIncrement)
            {
                ventPoints.TryGetValue((x, y), out int count);
                ventPoints[(x, y)] = count + 1;
            }
        }

        //for (int j = 0, n = ventPoints.Keys.Select(key => key.Y).Max(); j <= n; j++)
        //{
        //    for (int i = 0, m = ventPoints.Keys.Select(key => key.X).Max(); i <= m; i++)
        //    {
        //        if (ventPoints.TryGetValue((i, j), out int count))
        //        {
        //            Console.Write(count);
        //        }
        //        else
        //        {
        //            Console.Write('.');
        //        }
        //    }
        //    Console.WriteLine();
        //}

        return ventPoints.Count(entry => entry.Value >= 2);
    }
}
