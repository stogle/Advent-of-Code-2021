namespace AdventOfCode2021.BinaryDiagnostic;

public class BinaryDiagnostic2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var binaryDiagnostic = new BinaryDiagnostic2(reader);
        (int O2GeneratorRating, int CO2ScrubberRating) result = binaryDiagnostic.GetO2GeneratorAndCO2ScrubberRatings();
        Console.WriteLine(result.O2GeneratorRating * result.CO2ScrubberRating);
    }

    private readonly TextReader _reader;

    public BinaryDiagnostic2(TextReader reader)
    {
        _reader = reader;
    }

    public (int O2GeneratorRating, int CO2ScrubberRating) GetO2GeneratorAndCO2ScrubberRatings()
    {
        List<string> linesWith0 = new List<string>();
        List<string> linesWith1 = new List<string>();
        int count = 0;
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            if (line[0] == '0')
            {
                linesWith0.Add(line);
            }
            else
            {
                linesWith1.Add(line);
            }
            count++;
        }

        List<string> o2Lines = linesWith0.Count >= linesWith1.Count ? linesWith0 : linesWith1;
        int o2Bit = 1;
        while (o2Lines.Count > 1)
        {
            var lookup = o2Lines.ToLookup(line => line[o2Bit]);
            o2Lines = lookup['0'].Count() > lookup['1'].Count() ? lookup['0'].ToList() : lookup['1'].ToList();
            o2Bit++;
        }
        int o2GeneratorRating = Convert.ToInt32(o2Lines[0], 2);

        List<string> co2Lines = linesWith0.Count >= linesWith1.Count ? linesWith1 : linesWith0;
        int co2Bit = 1;
        while (co2Lines.Count > 1)
        {
            var lookup = co2Lines.ToLookup(line => line[co2Bit]);
            co2Lines = lookup['0'].Count() <= lookup['1'].Count() ? lookup['0'].ToList() : lookup['1'].ToList();
            co2Bit++;
        }
        int co2ScrubberRating = Convert.ToInt32(co2Lines[0], 2);

        return (o2GeneratorRating, co2ScrubberRating);
    }
}
