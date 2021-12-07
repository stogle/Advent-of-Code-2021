namespace AdventOfCode2021.ReactorReboot;

public class ReactorReboot2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var reactorReboot = new ReactorReboot2(reader);
        long result = reactorReboot.GetTotalCubesOn();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public ReactorReboot2(TextReader reader)
    {
        _reader = reader;
    }

    public long GetTotalCubesOn()
    {
        List<(bool On, Cube cube)> steps = new();
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            string[] parts = line.Split(" ");
            bool on = parts[0] == "on";
            string[] dimensions = parts[1].Split(",");
            long[] x = dimensions[0].Substring(2).Split("..").Select(long.Parse).ToArray();
            long[] y = dimensions[1].Substring(2).Split("..").Select(long.Parse).ToArray();
            long[] z = dimensions[2].Substring(2).Split("..").Select(long.Parse).ToArray();
            steps.Add((on, new Cube(x[0], x[1], y[0], y[1], z[0], z[1])));
        }

        List<Cube> cubes = new List<Cube>();
        foreach ((bool on, Cube cube) in steps)
        {
            var splitCubes = cubes.SelectMany(c => c.Split(cube));
            cubes = (on ? splitCubes.Append(cube) : splitCubes).ToList();
        }

        return cubes.Sum(c => c.Count);
    }

    private class Cube
    {
        private readonly long _x1;
        private readonly long _x2;
        private readonly long _y1;
        private readonly long _y2;
        private readonly long _z1;
        private readonly long _z2;

        public Cube(long x1, long x2, long y1, long y2, long z1, long z2)
        {
            if (x2 < x1) throw new ArgumentException("x2 must be greater than or equal to x1", nameof(x2));
            if (y2 < y1) throw new ArgumentException("y2 must be greater than or equal to y1", nameof(y2));
            if (z2 < z1) throw new ArgumentException("z2 must be greater than or equal to z1", nameof(z2));

            _x1 = x1;
            _x2 = x2;
            _y1 = y1;
            _y2 = y2;
            _z1 = z1;
            _z2 = z2;
        }

        public IEnumerable<Cube> Split(Cube other)
        {
            if (IsOverlapping(other))
            {
                // Split off non-overlapping sections on x-axis
                if (_x1 < other._x1)
                {
                    yield return new Cube(_x1, other._x1 - 1, _y1, _y2, _z1, _z2);
                }
                if (_x2 > other._x2)
                {
                    yield return new Cube(other._x2 + 1, _x2, _y1, _y2, _z1, _z2);
                }

                // Keep overlapping section on x-axis
                long x1 = Math.Max(_x1, other._x1);
                long x2 = Math.Min(_x2, other._x2);

                // Split off non-overlapping sections on y-axis
                if (_y1 < other._y1)
                {
                    yield return new Cube(x1, x2, _y1, other._y1 - 1, _z1, _z2);
                }
                if (_y2 > other._y2)
                {
                    yield return new Cube(x1, x2, other._y2 + 1, _y2, _z1, _z2);
                }

                // Keep overlapping section on y-axis
                long y1 = Math.Max(_y1, other._y1);
                long y2 = Math.Min(_y2, other._y2);

                // Split off non-overlapping sections on z-axis
                if (_z1 < other._z1)
                {
                    yield return new Cube(x1, x2, y1, y2, _z1, other._z1 - 1);
                }
                if (_z2 > other._z2)
                {
                    yield return new Cube(x1, x2, y1, y2, other._z2 + 1, _z2);
                }

                yield break;
            }

            yield return this;
        }

        private bool IsOverlapping(Cube other) =>
            (_x1 <= other._x2 && _x2 >= other._x1 || other._x1 <= _x2 && other._x2 >= _x1) &&
            (_y1 <= other._y2 && _y2 >= other._y1 || other._y1 <= _y2 && other._y2 >= _y1) &&
            (_z2 <= other._z2 && _z2 >= other._z1 || other._z1 <= _z2 && other._z2 >= _z1);

        public long Count => (_x2 - _x1 + 1) * (_y2 - _y1 + 1) * (_z2 - _z1 + 1);
    }
}
