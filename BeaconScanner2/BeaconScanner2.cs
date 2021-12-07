namespace AdventOfCode2021.BeaconScanner;

public class BeaconScanner2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var beaconScanner = new BeaconScanner2(reader);
        int result = beaconScanner.GetMaximumManhattanDistance();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public BeaconScanner2(TextReader reader)
    {
        _reader = reader;
    }

    public int GetMaximumManhattanDistance()
    {
        var scanners = new List<Scanner>();

        int index = 0;
        List<Point>? points = null;
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            if (line.StartsWith("---"))
            {
                if (points != null)
                {
                    scanners.Add(new Scanner(index++, points));
                }
                points = new List<Point>();
            }
            else if (line.Length != 0)
            {
                int[] coords = line.Split(",").Select(int.Parse).ToArray();
                points!.Add(new Point(coords[0], coords[1], coords[2]));
            }
        }
        scanners.Add(new Scanner(index, points!));

        // Find overlaps
        var overlaps = new Dictionary<Scanner, (Scanner Scanner, (Orientation Orientation, Point Offset) Transform)>();
        var scannersToMap = new HashSet<Scanner>(scanners.Skip(1));
        while (scannersToMap.Any())
        {
            foreach (var scanner1 in scannersToMap)
            {
                foreach (var scanner2 in overlaps.Keys.Prepend(scanners[0]))
                {
                    if (scanner2.IsOverlap(scanner1, out Orientation orientation, out Point offset))
                    {
                        overlaps[scanner1] = (scanner2, (orientation, offset));
                        scannersToMap.Remove(scanner1);
                        break;
                    }
                }
            }
        }

        // Find scanners
        var scannerPositions = new List<Point>();
        foreach (Scanner scanner in scanners)
        {
            Point point = new Point(0, 0, 0);
            Scanner s = scanner;
            var transforms = new List<(Orientation Orientation, Point Offset)>();
            while (overlaps.TryGetValue(s, out var value))
            {
                (Orientation orientation, Point offset) = value.Transform;
                point = orientation.Transform(point, offset);
                s = value.Scanner;
            }

            scannerPositions.Add(point);
        }

        return scannerPositions.SelectMany((b1, i) => scannerPositions
                .Skip(i + 1)
                .Select(b2 => Math.Abs(b2.X - b1.X) + Math.Abs(b2.Y - b1.Y) + Math.Abs(b2.Z - b1.Z)))
            .Max();
    }

    public record Point(int X, int Y, int Z);

    public class Scanner
    {
        private readonly int _index;
        private readonly IReadOnlyList<Point> _points;

        public Scanner(int index, IReadOnlyList<Point> points)
        {
            _index = index;
            _points = points;
        }

        public bool IsOverlap(Scanner scanner2, out Orientation orientation, out Point offset)
        {
            foreach (Point p1 in _points)
            {
                foreach (Point p2 in scanner2._points)
                {
                    foreach (Orientation o in Orientation.Values)
                    {
                        orientation = o;
                        offset = orientation.Transform(p2, p1);

                        if (IsOverlap(scanner2, orientation, offset))
                        {
                            return true;
                        }
                    }
                }
            }

            offset = new Point(0, 0, 0);
            orientation = Orientation.Default;
            return false;
        }

        private bool IsOverlap(Scanner scanner2, Orientation orientation, Point offset)
        {
            int overlapCount = 0;
            foreach (Point p2 in scanner2._points.Select(p => orientation.Transform(p, offset)))
            {
                foreach (Point p1 in _points)
                {
                    if (p1 == p2)
                    {
                        if (++overlapCount == 12)
                        {
                            return true;
                        }

                        break;
                    }
                }
            }

            return false;
        }

        public IEnumerable<Point> Transform(IEnumerable<(Orientation Orientation, Point Offset)> transforms)
        {
            foreach (var point in _points)
            {
                Point result = point;
                foreach ((Orientation orientation, Point offset) in transforms)
                {
                    result = orientation.Transform(result, offset);
                }
                yield return result;
            }
        }

        public override string ToString() => $"scanner {_index}";
    }

    public class Orientation
    {
        public static readonly Orientation Default = new("Facing: +Z, Up: +Y", p => p);
        public static readonly IReadOnlyList<Orientation> Values = new[]
        {
            Default,
            new Orientation("Facing: +Z, Up: -Y", p => new Point(-p.X, -p.Y, p.Z)),
            new Orientation("Facing: +Z, Up: +X", p => new Point(-p.Y, p.X, p.Z)),
            new Orientation("Facing: +Z, Up: -X", p => new Point(p.Y, -p.X, p.Z)),
            new Orientation("Facing: -Z, Up: +Y", p => new Point(-p.X, p.Y, -p.Z)),
            new Orientation("Facing: -Z, Up: -Y", p => new Point(p.X, -p.Y, -p.Z)),
            new Orientation("Facing: -Z, Up: +X", p => new Point(p.Y, p.X, -p.Z)),
            new Orientation("Facing: -Z, Up: -X", p => new Point(-p.Y, -p.X, -p.Z)),
            new Orientation("Facing: +Y, Up: +X", p => new Point(p.Z, p.X, p.Y)),
            new Orientation("Facing: +Y, Up: -X", p => new Point(-p.Z, -p.X, p.Y)),
            new Orientation("Facing: +Y, Up: +Z", p => new Point(-p.X, p.Z, p.Y)),
            new Orientation("Facing: +Y, Up: -Z", p => new Point(p.X, -p.Z, p.Y)),
            new Orientation("Facing: -Y, Up: +X", p => new Point(-p.Z, p.X, -p.Y)),
            new Orientation("Facing: -Y, Up: -X", p => new Point(p.Z, -p.X, -p.Y)),
            new Orientation("Facing: -Y, Up: +Z", p => new Point(p.X, p.Z, -p.Y)),
            new Orientation("Facing: -Y, Up: -Z", p => new Point(-p.X, -p.Z, -p.Y)),
            new Orientation("Facing: +X, Up: +Z", p => new Point(p.Y, p.Z, p.X)),
            new Orientation("Facing: +X, Up: -Z", p => new Point(-p.Y, -p.Z, p.X)),
            new Orientation("Facing: +X, Up: +Y", p => new Point(-p.Z, p.Y, p.X)),
            new Orientation("Facing: +X, Up: -Y", p => new Point(p.Z, -p.Y, p.X)),
            new Orientation("Facing: -X, Up: +Z", p => new Point(-p.Y, p.Z, -p.X)),
            new Orientation("Facing: -X, Up: -Z", p => new Point(p.Y, -p.Z, -p.X)),
            new Orientation("Facing: -X, Up: +Y", p => new Point(p.Z, p.Y, -p.X)),
            new Orientation("Facing: -X, Up: -Y", p => new Point(-p.Z, -p.Y, -p.X))
        };

        private readonly string _name;
        private readonly Func<Point, Point> _transform;

        private Orientation(string name, Func<Point, Point> transform)
        {
            _name = name;
            _transform = transform;
        }

        public Point Transform(Point point, Point offset)
        {
            (int x, int y, int z) = _transform(point);
            return new Point(x - offset.X, y - offset.Y, z - offset.Z);
        }

        public override string ToString() => _name;
    }

    public enum Axis
    {
        X,
        Y,
        Z
    }
}
