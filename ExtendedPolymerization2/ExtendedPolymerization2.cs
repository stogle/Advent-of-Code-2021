using System.Text;

namespace AdventOfCode2021.ExtendedPolymerization;

public class ExtendedPolymerization2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var passagePathing = new ExtendedPolymerization2(reader);
        (long MostCommonQuantity, long LeastCommonQuantity) result = passagePathing.GetMostCommonAndLeastCommonQuantities(40);
        Console.WriteLine(result.MostCommonQuantity - result.LeastCommonQuantity);
    }

    private readonly TextReader _reader;

    public ExtendedPolymerization2(TextReader reader)
    {
        _reader = reader;
    }

    public (long MostCommonQuantity, long LeastCommonQuantity) GetMostCommonAndLeastCommonQuantities(int steps)
    {
        IDictionary<char, long> charCounts = new Dictionary<char, long>();
        IDictionary<string, long> pairCounts = new Dictionary<string, long>();
        string polymer = _reader.ReadLine()!;
        for (int i = 0; i < polymer.Length; i++)
        {
            char c = polymer[i];
            charCounts.TryGetValue(c, out long count);
            charCounts[c] = count + 1;
        }
        for (int i = 0; i < polymer.Length - 1; i++)
        {
            string pair = polymer.Substring(i, 2);
            pairCounts.TryGetValue(pair, out long count);
            pairCounts[pair] = count + 1;
        }
        _reader.ReadLine();

        IDictionary<string, string> rules = new Dictionary<string, string>();
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            string[] parts = line.Split(" -> ");
            rules[parts[0]] = parts[1];
        }

        for (int step = 0; step < steps; step++)
        {
            IDictionary<string, long> newPairCounts = new Dictionary<string, long>();
            foreach (var entry in pairCounts)
            {
                string elementToInsert = rules[entry.Key];
                charCounts.TryGetValue(elementToInsert[0], out long count1);
                charCounts[elementToInsert[0]] = count1 + entry.Value;
                string pair1 = $"{entry.Key[0]}{elementToInsert}";
                newPairCounts.TryGetValue(pair1, out long count2);
                newPairCounts[pair1] = count2 + entry.Value;
                string pair2 = $"{elementToInsert}{entry.Key[1]}";
                newPairCounts.TryGetValue(pair2, out long count3);
                newPairCounts[pair2] = count3 + entry.Value;
            }
            pairCounts = newPairCounts;
        }

        long mostCommonQuantity = charCounts.Max(entry => entry.Value);
        long leastCommonQuantity = charCounts.Min(entry => entry.Value);
        return (mostCommonQuantity, leastCommonQuantity);
    }
}
