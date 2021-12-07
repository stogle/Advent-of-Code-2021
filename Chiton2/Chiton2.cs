namespace AdventOfCode2021.Chiton;

public class Chiton2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var chiton = new Chiton2(reader);
        int result = chiton.GetLowestTotalRisk();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public Chiton2(TextReader reader)
    {
        _reader = reader;
    }

    public int GetLowestTotalRisk()
    {
        List<int[]> rows = new List<int[]>();
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            rows.Add(line.Select(c => int.Parse(c.ToString())).ToArray());
        }
        int[][] riskLevels = Enumerable.Range(0, 5 * rows.Count)
            .Select(row => Enumerable.Range(0, 5 * rows[row % rows.Count].Length)
                .Select(column => (rows[row % rows.Count][column % rows[row % rows.Count].Length] +
                                   row / rows.Count + column / rows[row % rows.Count].Length - 1) % 9 + 1)
                .ToArray())
            .ToArray();

        IDictionary<(int Row, int Column), int> totalRiskLevels = new Dictionary<(int Row, int Column), int>(Enumerable.Range(0, riskLevels.Length)
            .SelectMany(row => Enumerable.Range(0, riskLevels[row].Length)
                .Select(column => new KeyValuePair<(int Row, int Column), int>((row, column), row == 0 && column == 0 ? 0 : int.MaxValue))));
        var unvisitedNodes = new SortedSet<(int RiskLevel, int Row, int Column)>() { (0, 0, 0) };

        while (unvisitedNodes.Any())
        {
            (int riskLevel, int row, int column) = unvisitedNodes.First();

            if (row == riskLevels.Length - 1 && column == riskLevels[row].Length - 1)
            {
                // Reached destination
                return riskLevel;
            }

            if (row > 0) // Up
            {
                Update(row - 1, column, riskLevel + riskLevels[row - 1][column]);
            }
            if (row < riskLevels.Length - 1) // Down
            {
                Update(row + 1, column, riskLevel + riskLevels[row + 1][column]);
            }
            if (column > 0) // Left
            {
                Update(row, column - 1, riskLevel + riskLevels[row][column - 1]);
            }
            if (column < riskLevels[row].Length - 1) // Right
            {
                Update(row, column + 1, riskLevel + riskLevels[row][column + 1]);
            }

            unvisitedNodes.Remove((riskLevel, row, column));
        }

        return int.MaxValue;

        void Update(int row, int column, int newRiskLevel)
        {
            int totalRiskLevel = totalRiskLevels[(row, column)];
            if (newRiskLevel < totalRiskLevel)
            {
                totalRiskLevels[(row, column)] = newRiskLevel;
                unvisitedNodes.Remove((totalRiskLevel, row, column));
                unvisitedNodes.Add((newRiskLevel, row, column));
            }
        }
    }
}
