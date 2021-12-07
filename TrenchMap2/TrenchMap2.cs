using System.Text;

namespace AdventOfCode2021.TrenchMap;

public class TrenchMap2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var trenchMap = new TrenchMap2(reader);
        int result = trenchMap.GetEnhancedImage(50).GetLitPixelCount();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public TrenchMap2(TextReader reader)
    {
        _reader = reader;
    }

    public Image GetEnhancedImage(int enhancementCount)
    {
        bool[] algorithm = _reader.ReadLine()!.Select(c => c == '#').ToArray();
        _reader.ReadLine();

        Image image = new Image();
        int row = 0;
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            for (int column = 0; column < line.Length; column++)
            {
                if (line[column] == '#')
                {
                    image.LightPixel(row, column);
                }
            }
            row++;
        }

        for (int i = 0; i < enhancementCount; i++)
        {
            image = image.Enhance(algorithm);
        }

        return image;
    }

    public class Image
    {
        private readonly ISet<(int Row, int Column)> _litPixels = new HashSet<(int Row, int Column)>();
        private bool _isInverted;
        private int _rowMin;
        private int _rowMax;
        private int _columnMin;
        private int _columnMax;

        public void LightPixel(int row, int column)
        {
            _litPixels.Add((row, column));
            _rowMin = Math.Min(_rowMin, row);
            _rowMax = Math.Max(_rowMax, row);
            _columnMin = Math.Min(_columnMin, column);
            _columnMax = Math.Max(_columnMax, column);
        }

        public Image Enhance(bool[] algorithm)
        {
            var result = new Image();
            for (int row = _rowMin - 1; row <= _rowMax + 1; row++)
            {
                for (int column = _columnMin - 1; column <= _columnMax + 1; column++)
                {
                    bool isLit = algorithm[GetEnhancementIndex(row, column)];
                    if (isLit)
                    {
                        result.LightPixel(row, column);
                    }
                }
            }
            result._isInverted = _isInverted ? algorithm[0b111111111] : algorithm[0b000000000];

            return result;
        }

        private int GetEnhancementIndex(int row, int column)
        {
            char[] binaryValue = new char[9];
            int index = 0;
            for (int neighbourRow = row - 1; neighbourRow <= row + 1; neighbourRow++)
            {
                for (int neighbourColumn = column - 1; neighbourColumn <= column + 1; neighbourColumn++)
                {
                    binaryValue[index++] = IsLit(neighbourRow, neighbourColumn) ? '1' : '0';
                }
            }

            return Convert.ToInt32(new string(binaryValue), 2);
        }

        private bool IsLit(int row, int column)
        {
            return _litPixels.Contains((row, column)) ||
                   _isInverted && (row < _rowMin || row > _rowMax || column < _columnMin || column > _columnMax);
        }

        public int GetLitPixelCount()
        {
            return _isInverted ? int.MaxValue : _litPixels.Count;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (int row = _rowMin; row <= _rowMax; row++)
            {
                for (int column = _columnMin; column <= _columnMax; column++)
                {
                    result.Append(IsLit(row, column) ? '#' : '.');
                }

                result.AppendLine();
            }

            return result.ToString();
        }
    }
}
