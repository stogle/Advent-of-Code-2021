namespace AdventOfCode2021.TransparentOrigami;

public class TransparentOrigami2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var passagePathing = new TransparentOrigami2(reader);
        string result = passagePathing.GetFoldedOutput();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public TransparentOrigami2(TextReader reader)
    {
        _reader = reader;
    }

    public string GetFoldedOutput()
    {
        ISet<(int X, int Y)> dots = new HashSet<(int X, int Y)>();
        List<(bool IsHorizontal, int Position)> folds = new List<(bool IsVertical, int Position)>();
        bool readDots = false;
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            if (line == String.Empty)
            {
                readDots = true;
            }
            else if (!readDots)
            {
                int[] coordinates = line.Split(',').Select(int.Parse).ToArray();
                dots.Add((coordinates[0], coordinates[1]));
            }
            else
            {
                string[] instruction = line.Split('=');
                bool isHorizontal = instruction[0].EndsWith("y");
                int position = int.Parse(instruction[1]);
                folds.Add((isHorizontal, position));
            }
        }

        foreach (var fold in folds)
        {
            foreach (var dot in dots.ToList())
            {
                if (fold.IsHorizontal ? fold.Position < dot.Y : fold.Position < dot.X)
                {
                    int x = fold.IsHorizontal ? dot.X : fold.Position - (dot.X - fold.Position);
                    int y = fold.IsHorizontal ? fold.Position - (dot.Y - fold.Position) : dot.Y;
                    dots.Remove(dot);
                    dots.Add((x, y));
                }
            }
        }

        // .##..#....###..####.#..#.####.#..#.#..#
        // #..#.#....#..#.#....#.#..#....#.#..#..#
        // #..#.#....#..#.###..##...###..##...#..#
        // ####.#....###..#....#.#..#....#.#..#..#
        // #..#.#....#.#..#....#.#..#....#.#..#..#
        // #..#.####.#..#.####.#..#.#....#..#..##.

        int width = dots.Max(dot => dot.X);
        int height = dots.Max(dot => dot.Y);
        return string.Join("\r\n", Enumerable.Range(0, height + 1)
            .Select(y => string.Join("", Enumerable.Range(0, width + 1)
                .Select(x => dots.Contains((x, y)) ? "#" : "."))));
    }
}
