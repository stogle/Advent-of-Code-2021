namespace AdventOfCode2021.SyntaxScoring;

public class SyntaxScoring
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var syntaxScoring = new SyntaxScoring(reader);
        int result = syntaxScoring.GetTotalSyntaxErrorScore();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public SyntaxScoring(TextReader reader)
    {
        _reader = reader;
    }

    private static readonly IDictionary<char, int> _score = new Dictionary<char, int>
    {
        { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 }
    };

    public int GetTotalSyntaxErrorScore()
    {
        int result = 0;
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            Stack<char> openChunks = new Stack<char>();
            foreach (var ch in line)
            {
                switch (ch)
                {
                    case ')':
                        if (openChunks.Pop() == '(')
                        {
                            continue;
                        }
                        break;
                    case ']':
                        if (openChunks.Pop() == '[')
                        {
                            continue;
                        }
                        break;
                    case '}':
                        if (openChunks.Pop() == '{')
                        {
                            continue;
                        }
                        break;
                    case '>':
                        if (openChunks.Pop() == '<')
                        {
                            continue;
                        }
                        break;
                    default:
                        openChunks.Push(ch);
                        continue;
                }

                result += _score[ch];
            }
        }

        return result;
    }
}
