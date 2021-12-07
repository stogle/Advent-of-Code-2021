namespace AdventOfCode2021.Lanternfish;

public class Lanternfish2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var lanternfish = new Lanternfish2(reader);
        long result = lanternfish.GetTotal(256);
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public Lanternfish2(TextReader reader)
    {
        _reader = reader;
    }

    public long GetTotal(int days)
    {
        long[] countByAge = new long[9];
        int c;
        while ((c = _reader.Read()) != -1)
        {
            long age = long.Parse(((char)c).ToString());
            countByAge[age]++;
            _ = _reader.Read();
        }

        for (int i = 0; i < days; i++)
        {
            countByAge = new[] { countByAge[1], countByAge[2], countByAge[3], countByAge[4], countByAge[5], countByAge[6], countByAge[0] + countByAge[7], countByAge[8], countByAge[0] };
        }

        return countByAge.Sum();
    }
}
