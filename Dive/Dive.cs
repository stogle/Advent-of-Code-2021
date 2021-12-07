namespace AdventOfCode2021.Dive;

public class Dive
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var dive = new Dive(reader);
        (int Horizontal, int Depth) result = dive.GetHorizontalAndDepth();
        Console.WriteLine(result.Horizontal * result.Depth);
    }

    private readonly TextReader _reader;

    public Dive(TextReader reader)
    {
        _reader = reader;
    }

    public (int Horizontal, int Depth) GetHorizontalAndDepth()
    {
        (int Horizontal, int Depth) result = (0, 0);
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            string[] split = line.Split(' ');
            string command = split[0];
            int units = int.Parse(split[1]);
            switch (command)
            {
                case "forward":
                    result.Horizontal += units;
                    break;
                case "down":
                    result.Depth += units;
                    break;
                case "up":
                    result.Depth -= units;
                    break;
            }
        }

        return result;
    }
}
