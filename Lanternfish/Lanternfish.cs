namespace AdventOfCode2021.Lanternfish;

public class Lanternfish
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var lanternfish = new Lanternfish(reader);
        int result = lanternfish.GetTotal(80);
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public Lanternfish(TextReader reader)
    {
        _reader = reader;
    }

    public int GetTotal(int days)
    {
        List<int> ages = _reader.ReadLine()!.Split(',').Select(int.Parse).ToList();

        for (int i = 0; i < days; i++)
        {
            ages = ages.Select(age => age == 0 ? 6 : age - 1)
                .Concat(Enumerable.Repeat(8, ages.Count(age => age == 0)))
                .ToList();
        }

        return ages.Count;
    }
}
