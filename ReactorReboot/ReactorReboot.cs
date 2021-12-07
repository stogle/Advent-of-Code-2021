namespace AdventOfCode2021.ReactorReboot;

public class ReactorReboot
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var reactorReboot = new ReactorReboot(reader);
        int result = reactorReboot.GetTotalCubesOn();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public ReactorReboot(TextReader reader)
    {
        _reader = reader;
    }

    public int GetTotalCubesOn()
    {
        List<(bool On, int X1, int X2, int Y1, int Y2, int Z1, int Z2)> steps = new();
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            string[] parts = line.Split(" ");
            bool on = parts[0] == "on";
            string[] dimensions = parts[1].Split(",");
            int[] x = dimensions[0].Substring(2).Split("..").Select(int.Parse).ToArray();
            int[] y = dimensions[1].Substring(2).Split("..").Select(int.Parse).ToArray();
            int[] z = dimensions[2].Substring(2).Split("..").Select(int.Parse).ToArray();
            steps.Add((on, x[0], x[1], y[0], y[1], z[0], z[1]));
        }

        bool[,,] reactor = new bool[101, 101, 101];
        foreach ((bool on, int x1, int x2, int y1, int y2, int z1, int z2) in steps)
        {
            for (int x = Math.Max(-50, x1); x <= Math.Min(x2, 50); x++)
            {
                for (int y = Math.Max(-50, y1); y <= Math.Min(y2, 50); y++)
                {
                    for (int z = Math.Max(-50, z1); z <= Math.Min(z2, 50); z++)
                    {
                        reactor[50 + x, 50 + y, 50 + z] = on;
                    }
                }
            }
        }

        return reactor.Cast<bool>().Count(b => b);
    }
}
