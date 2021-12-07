namespace AdventOfCode2021.Chiton;

public class Chiton
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var chiton = new Chiton(reader);
        int result = chiton.GetLowestTotalRisk();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public Chiton(TextReader reader)
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
        int[][] riskLevels = rows.ToArray();

        IDictionary<(int Row, int Column), int> totalRiskLevels = new Dictionary<(int Row, int Column), int>(Enumerable.Range(0, riskLevels.Length)
            .SelectMany(row => Enumerable.Range(0, riskLevels[row].Length)
                .Select(column => new KeyValuePair<(int Row, int Column), int>((row, column), row == 0 && column == 0 ? 0 : int.MaxValue))));
        ISet<(int Row, int Column)> unvisitedNodes = totalRiskLevels.Keys.ToHashSet();

        while (unvisitedNodes.Any())
        {
            (int row, int column) = unvisitedNodes.MinBy(node => totalRiskLevels[node]);
            int riskLevel = totalRiskLevels[(row, column)];

            if (row == riskLevels.Length - 1 && column == riskLevels[row].Length - 1)
            {
                // Reached destination
                return riskLevel;
            }

            if (row > 0) // Up
            {
                totalRiskLevels[(row - 1, column)] = Math.Min(totalRiskLevels[(row - 1, column)], riskLevel + riskLevels[row - 1][column]);
            }
            if (row < riskLevels.Length - 1) // Down
            {
                totalRiskLevels[(row + 1, column)] = Math.Min(totalRiskLevels[(row + 1, column)], riskLevel + riskLevels[row + 1][column]);
            }
            if (column > 0) // Left
            {
                totalRiskLevels[(row, column - 1)] = Math.Min(totalRiskLevels[(row, column - 1)], riskLevel + riskLevels[row][column - 1]);
            }
            if (column < riskLevels[row].Length - 1) // Right
            {
                totalRiskLevels[(row, column + 1)] = Math.Min(totalRiskLevels[(row, column + 1)], riskLevel + riskLevels[row][column + 1]);
            }

            unvisitedNodes.Remove((row, column));
        }

        return int.MaxValue;
    }
}
