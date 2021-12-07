using System.Text;

namespace AdventOfCode2021.ExtendedPolymerization;

public class ExtendedPolymerization
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var passagePathing = new ExtendedPolymerization(reader);
        (int MostCommonQuantity, int LeastCommonQuantity) result = passagePathing.GetMostCommonAndLeastCommonQuantities(10);
        Console.WriteLine(result.MostCommonQuantity - result.LeastCommonQuantity);
    }

    private readonly TextReader _reader;

    public ExtendedPolymerization(TextReader reader)
    {
        _reader = reader;
    }

    public (int MostCommonQuantity, int LeastCommonQuantity) GetMostCommonAndLeastCommonQuantities(int steps)
    {
        string polymer = _reader.ReadLine()!;
        IDictionary<string, string> rules = new Dictionary<string, string>();

        _reader.ReadLine();
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            string[] parts = line.Split(" -> ");
            rules[parts[0]] = parts[1];
        }

        for (int step = 0; step < steps; step++)
        {
            var output = new StringBuilder();
            for (int i = 0; i < polymer.Length - 1; i++)
            {
                string pair = polymer.Substring(i, 2);
                output.Append($"{polymer[i]}{rules[pair]}");
            }
            output.Append(polymer.Last());
            polymer = output.ToString();
        }

        var groups = polymer.GroupBy(ch => ch);
        int mostCommonQuantity = groups.Max(g => g.Count());
        int leastCommonQuantity = groups.Min(g => g.Count());
        return (mostCommonQuantity, leastCommonQuantity);
    }
}
