namespace AdventOfCode2021.BinaryDiagnostic;

public class BinaryDiagnostic
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var binaryDiagnostic = new BinaryDiagnostic(reader);
        (int Gamma, int Epsilon) result = binaryDiagnostic.GetGammaAndEpsilon();
        Console.WriteLine(result.Gamma * result.Epsilon);
    }

    private readonly TextReader _reader;

    public BinaryDiagnostic(TextReader reader)
    {
        _reader = reader;
    }

    public (int Gamma, int Epsilon) GetGammaAndEpsilon()
    {
        (int Gamma, int Epsilon) result = (0, 0);
        int count = 0;
        List<int> bitCount = new List<int>();
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            while (bitCount.Count < line.Length)
            {
                bitCount.Add(0);
            }

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '1')
                {
                    bitCount[i]++;
                }
            }
            count++;
        }

        for (int i = 0; i < bitCount.Count; i++)
        {
            result.Gamma <<= 1;
            result.Epsilon <<= 1;
            if (2 * bitCount[i] > count)
            {
                result.Gamma++;
            }
            else
            {
                result.Epsilon++;
            }
        }

        return result;
    }
}
