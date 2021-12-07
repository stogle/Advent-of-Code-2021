namespace AdventOfCode2021.TreacheryOfWhales;

public class TreacheryOfWhales2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var treacheryOfWhales = new TreacheryOfWhales2(reader);
        int result = treacheryOfWhales.GetFuel();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public TreacheryOfWhales2(TextReader reader)
    {
        _reader = reader;
    }

    public int GetFuel()
    {
        int[] positions = _reader.ReadLine()!.Split(',')
            .Select(int.Parse)
            .OrderBy(p => p)
            .ToArray();

        int min = positions[0];
        int max = positions[positions.Length - 1];
        return Enumerable.Range(min, max - min + 1)
            .Select(m => positions.Sum(p => Math.Abs(p - m) * (Math.Abs(p - m) + 1) / 2))
            .Min();
    }
}
