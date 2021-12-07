namespace AdventOfCode2021.PassagePathing;

public class PassagePathing
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var passagePathing = new PassagePathing(reader);
        int result = passagePathing.GetTotalPaths();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public PassagePathing(TextReader reader)
    {
        _reader = reader;
    }

    public int GetTotalPaths()
    {
        IDictionary<string, Cave> caves = new Dictionary<string, Cave>();
        string? line;
        while((line = _reader.ReadLine()) != null)
        {
            string[] ends = line.Split('-');
            string cave1Name = ends[0];
            string cave2Name = ends[1];
            if (!caves.TryGetValue(cave1Name, out Cave? cave1))
            {
                cave1 = new Cave(cave1Name, char.IsLower(cave1Name.First()));
                caves[cave1Name] = cave1;
            }
            if (!caves.TryGetValue(ends[1], out Cave? cave2))
            {
                cave2 = new Cave(cave2Name, char.IsLower(cave2Name.First()));
                caves[cave2Name] = cave2;
            }
            cave1.Connect(cave2);
            cave2.Connect(cave1);
        }

        Cave start = caves["start"];
        return GetTotalPaths(start, new[] { start }, caves["end"]);
    }

    private int GetTotalPaths(Cave cave, IEnumerable<Cave> visitedSmallCaves, Cave end)
    {
        if (cave == end)
        {
            return 1;
        }

        int result = 0;
        foreach (var next in cave.Caves.Except(visitedSmallCaves))
        {
            IEnumerable<Cave> visited = next.IsSmall ?
                visitedSmallCaves.Append(next) :
                visitedSmallCaves;
            result += GetTotalPaths(next, visited, end);
        }
        return result;
    }

    private class Cave
    {
        public string Name { get; }
        public bool IsSmall { get; }
        public IList<Cave> Caves { get; } = new List<Cave>();

        public Cave(string name, bool isSmall)
        {
            Name = name;
            IsSmall = isSmall;
        }

        public void Connect(Cave cave)
        {
            Caves.Add(cave);
        }
    }
}
