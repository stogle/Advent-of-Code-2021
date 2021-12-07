namespace AdventOfCode2021.SonarSweep;

public class SonarSweep
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var sonarSweep = new SonarSweep(reader);
        int result = sonarSweep.GetNumberOfIncreases();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public SonarSweep(TextReader reader)
    {
        _reader = reader;
    }

    public int GetNumberOfIncreases()
    {
        string? line;
        int result = 0;
        int? previousDepth = null;
        while ((line = _reader.ReadLine()) != null)
        {
            int depth = int.Parse(line);
            if (depth > previousDepth)
            {
                result++;
            }
            previousDepth = depth;
        }

        return result;
    }
}
