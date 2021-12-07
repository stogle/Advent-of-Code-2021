namespace AdventOfCode2021.Dive;

public class Dive2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var dive = new Dive2(reader);
        (int Horizontal, int Aim, int Depth) result = dive.GetHorizontalAndAimAndAndDepth();
        Console.WriteLine(result.Horizontal * result.Depth);
    }

    private readonly TextReader _reader;

    public Dive2(TextReader reader)
    {
        _reader = reader;
    }

    public (int Horizontal, int Aim, int Depth) GetHorizontalAndAimAndAndDepth()
    {
        (int Horizontal, int Aim, int Depth) result = (0, 0, 0);
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
                    result.Depth += result.Aim * units;
                    break;
                case "down":
                    result.Aim += units;
                    break;
                case "up":
                    result.Aim -= units;
                    break;
            }
        }

        return result;
    }
}
