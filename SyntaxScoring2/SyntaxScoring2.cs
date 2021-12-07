namespace AdventOfCode2021.SyntaxScoring;

public class SyntaxScoring2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var syntaxScoring = new SyntaxScoring2(reader);
        long result = syntaxScoring.GetMiddleAutocompleteScore();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public SyntaxScoring2(TextReader reader)
    {
        _reader = reader;
    }

    private static readonly IDictionary<char, long> _score = new Dictionary<char, long>
    {
        { '(', 1L }, { '[', 2L }, { '{', 3L }, { '<', 4L }
    };

    public long GetMiddleAutocompleteScore()
    {
        var result = new List<long>();
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            long? score = GetAutocompleteScore(line);
            if (score.HasValue)
            {
                result.Add(score.Value);
            }
        }

        // 554214793 is too low
        return result.OrderBy(r => r).Skip(result.Count / 2).First();
    }

    private long? GetAutocompleteScore(string line)
    {
        Stack<char> openChunks = new Stack<char>();
        foreach (var ch in line)
        {
            switch (ch)
            {
                case ')':
                    if (openChunks.Pop() != '(')
                    {
                        return null;
                    }
                    break;
                case ']':
                    if (openChunks.Pop() != '[')
                    {
                        return null;
                    }
                    break;
                case '}':
                    if (openChunks.Pop() != '{')
                    {
                        return null;
                    }
                    break;
                case '>':
                    if (openChunks.Pop() != '<')
                    {
                        return null;
                    }
                    break;
                default:
                    openChunks.Push(ch);
                    break;
            }
        }

        return openChunks.Aggregate(0L, (result, ch) => result = result * 5L + _score[ch]);
    }
}
