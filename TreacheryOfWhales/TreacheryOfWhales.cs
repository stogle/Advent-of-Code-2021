namespace AdventOfCode2021.TreacheryOfWhales;

public class TreacheryOfWhales
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var treacheryOfWhales = new TreacheryOfWhales(reader);
        int result = treacheryOfWhales.GetFuel();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public TreacheryOfWhales(TextReader reader)
    {
        _reader = reader;
    }

    public int GetFuel()
    {
        int[] positions = _reader.ReadLine()!.Split(',')
            .Select(int.Parse)
            .OrderBy(p => p)
            .ToArray();
        int median = positions[positions.Length / 2];

        return positions.Sum(p => Math.Abs(median - p));
    }
}
