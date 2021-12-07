namespace AdventOfCode2021.SevenSegmentSearch;

public class SevenSegmentSearch2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var sevenSegmentSearch = new SevenSegmentSearch2(reader);
        int result = sevenSegmentSearch.GetTotalOutputValues();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public SevenSegmentSearch2(TextReader reader)
    {
        _reader = reader;
    }

    public int GetTotalOutputValues()
    {
        int result = 0;
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            string[] entry = line.Split('|', StringSplitOptions.TrimEntries);
            char[][] signalPatterns = entry[0].Split(' ').Select(s => s.ToCharArray().OrderBy(c => c).ToArray()).ToArray();
            char[][] output = entry[1].Split(' ').Select(s => s.ToCharArray().OrderBy(c => c).ToArray()).ToArray();

            // one, four, seven and eight have unique number of segments
            char[] onePattern = signalPatterns.First(s => s.Length == 2);
            char[] fourPattern = signalPatterns.First(s => s.Length == 4);
            char[] sevenPattern = signalPatterns.First(s => s.Length == 3);
            char[] eightPattern = signalPatterns.First(s => s.Length == 7);

            // two, three, and five all have 5 segments
            char[] twoPattern = signalPatterns.First(s => s.Length == 5 && s.Intersect(fourPattern).Count() == 2);
            char[] threePattern = signalPatterns.First(s => s.Length == 5 && s.Intersect(onePattern).Count() == 2);
            char[] fivePattern = signalPatterns.First(s => s.Length == 5 && s.Intersect(twoPattern).Count() == 3);

            // zero, six, and nine all have 6 segments
            char[] zeroPattern = signalPatterns.First(s => s.Length == 6 && s.Intersect(fivePattern).Count() == 4);
            char[] sixPattern = signalPatterns.First(s => s.Length == 6 && s.Intersect(onePattern).Count() == 1);
            char[] ninePattern = signalPatterns.First(s => s.Length == 6 && s.Intersect(fourPattern).Count() == 4);

            int outputValue = 0;
            foreach (char[] digit in output)
            {
                if (digit.SequenceEqual(zeroPattern))
                    outputValue = outputValue * 10;
                else if (digit.SequenceEqual(onePattern))
                    outputValue = outputValue * 10 + 1;
                else if (digit.SequenceEqual(twoPattern))
                    outputValue = outputValue * 10 + 2;
                else if (digit.SequenceEqual(threePattern))
                    outputValue = outputValue * 10 + 3;
                else if (digit.SequenceEqual(fourPattern))
                    outputValue = outputValue * 10 + 4;
                else if (digit.SequenceEqual(fivePattern))
                    outputValue = outputValue * 10 + 5;
                else if (digit.SequenceEqual(sixPattern))
                    outputValue = outputValue * 10 + 6;
                else if (digit.SequenceEqual(sevenPattern))
                    outputValue = outputValue * 10 + 7;
                else if (digit.SequenceEqual(eightPattern))
                    outputValue = outputValue * 10 + 8;
                else if (digit.SequenceEqual(ninePattern))
                    outputValue = outputValue * 10 + 9;
                else
                    throw new ArgumentOutOfRangeException($"Unknown output vaue: {digit}");
            }
            result += outputValue;
        }

        return result;
    }
}
