namespace AdventOfCode2021.SonarSweep;

public class SonarSweep2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var sonarSweep = new SonarSweep2(reader);
        int result = sonarSweep.GetNumberOfSlidingWindowIncreases(3);
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public SonarSweep2(TextReader reader)
    {
        _reader = reader;
    }

    public int GetNumberOfSlidingWindowIncreases(int slidingWindowSize)
    {
        LinkedList<int> slidingWindow = new LinkedList<int>();
        string? line;
        while (slidingWindow.Count < slidingWindowSize && (line = _reader.ReadLine()) != null)
        {
            int depth = int.Parse(line);
            slidingWindow.AddLast(depth);
        }

        int result = 0;
        if (slidingWindow.Count == slidingWindowSize)
        {
            double previousAverage = slidingWindow.Average();
            while ((line = _reader.ReadLine()) != null)
            {
                int depth = int.Parse(line);
                slidingWindow.RemoveFirst();
                slidingWindow.AddLast(depth);
                double average = slidingWindow.Average();
                if (average > previousAverage)
                {
                    result++;
                }
                previousAverage = average;
            }
        }

        return result;
    }
}
