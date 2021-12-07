namespace AdventOfCode2021.SevenSegmentSearch;

public class SevenSegmentSearch
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var sevenSegmentSearch = new SevenSegmentSearch(reader);
        int result = sevenSegmentSearch.GetUniqueNumberOfSegmentsDigitCount();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public SevenSegmentSearch(TextReader reader)
    {
        _reader = reader;
    }

    private static int[] UniqueNumbersOfSegments = { 2, 3, 4, 7 };

    public int GetUniqueNumberOfSegmentsDigitCount()
    {
        int result = 0;
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            string[] inputOutput = line.Split('|', StringSplitOptions.TrimEntries);
            string[] output = inputOutput[1].Split(' ');
            result += output.Select(s => s.Length).Where(n => UniqueNumbersOfSegments.Contains(n)).Count();
        }

        return result;
    }
}
