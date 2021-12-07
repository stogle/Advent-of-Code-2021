using System.Diagnostics;

namespace AdventOfCode2021.Snailfish;

public class Snailfish2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var snailfish = new Snailfish2(reader);
        int result = snailfish.GetMaxMagnitudeOfSumOfPairs();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public Snailfish2(TextReader reader)
    {
        _reader = reader;
    }

    public int GetMaxMagnitudeOfSumOfPairs()
    {
        List<Number> numbers = new List<Number>();
        string? line;
        while ((line = _reader.ReadLine()) != null)
        {
            numbers.Add(Number.Parse(line));
        }

        return numbers.Max(x => numbers
            .Where(y => y != x)
            .Max(y => x.Add(y).Magnitude));
    }

    public class Number
    {
        public static Number Parse(string input)
        {
            int index = 0;
            return Parse(input, ref index);
        }

        private static Number Parse(string input, ref int index)
        {
            Debug.Assert(input[index++] == '[');
            Number? left;
            int? leftValue;
            if (int.TryParse(input.Substring(index, 1), out int value))
            {
                left = null;
                leftValue = value;
                index++;
            }
            else
            {
                left = Parse(input, ref index);
                leftValue = null;
            }
            Debug.Assert(input[index++] == ',');
            Number? right;
            int? rightValue;
            if (int.TryParse(input.Substring(index, 1), out value))
            {
                right = null;
                rightValue = value;
                index++;
            }
            else
            {
                right = Parse(input, ref index);
                rightValue = null;
            }
            Debug.Assert(input[index++] == ']');

            return new Number(left, leftValue, right, rightValue);
        }

        public Number? Left { get; }
        public int? LeftValue { get; }
        public Number? Right { get; }
        public int? RightValue { get; }

        public Number(Number? left, int? leftValue, Number? right, int? rightValue)
        {
            Left = left;
            LeftValue = leftValue;
            Right = right;
            RightValue = rightValue;
        }

        public Number Add(Number other) =>
            Reduce(new Number(this, null, other, null));

        private static Number Reduce(Number number)
        {
            Number previous;
            do
            {
                previous = number;
                number = number.Explode();
                if (number == previous)
                {
                    number = number.Split();
                }
            } while (number != previous);

            return number;
        }

        private Number Explode()
        {
            return Explode(0, out _, out _);
        }

        private Number Explode(int depth, out int carryLeftValue, out int carryRightValue)
        {
            if (depth == 3)
            {
                if (Left != null)
                {
                    int rightValue = Left.RightValue!.Value;
                    carryLeftValue = Left.LeftValue!.Value;
                    carryRightValue = 0;
                    return new Number(null, 0, Right?.AddLeft(rightValue), RightValue + rightValue);
                }

                if (Right != null)
                {
                    int leftValue = Right.LeftValue!.Value;
                    carryLeftValue = 0;
                    carryRightValue = Right.RightValue!.Value;
                    return new Number(Left?.AddRight(leftValue), LeftValue + leftValue, null, 0);
                }

                carryLeftValue = 0;
                carryRightValue = 0;
                return this;
            }

            if (Left != null)
            {
                Number leftExplode = Left.Explode(depth + 1, out carryLeftValue, out carryRightValue);
                if (leftExplode != Left)
                {
                    int rightValue = carryRightValue;
                    carryRightValue = 0;
                    return new Number(leftExplode, LeftValue, Right?.AddLeft(rightValue), RightValue + rightValue);
                }
            }

            if (Right != null)
            {
                Number rightExplode = Right.Explode(depth + 1, out carryLeftValue, out carryRightValue);
                if (rightExplode != Right)
                {
                    int leftValue = carryLeftValue;
                    carryLeftValue = 0;
                    return new Number(Left?.AddRight(leftValue), LeftValue + leftValue, rightExplode, RightValue);
                }
            }

            carryLeftValue = 0;
            carryRightValue = 0;
            return this;
        }

        private Number AddLeft(int value) =>
            new(Left?.AddLeft(value), LeftValue + value, Right, RightValue);

        private Number AddRight(int value) =>
            new(Left, LeftValue, Right?.AddRight(value), RightValue + value);

        private Number Split()
        {
            if (Left != null)
            {
                Number leftSplit = Left.Split();
                if (leftSplit != Left)
                {
                    return new Number(leftSplit, null, Right, RightValue);
                }
            }
            else if (LeftValue >= 10)
            {
                var leftSplit = new Number(null, LeftValue.Value / 2, null, (LeftValue.Value + 1) / 2);
                return new Number(leftSplit, null, Right, RightValue);
            }

            if (Right != null)
            {
                Number rightSplit = Right.Split();
                if (rightSplit != Right)
                {
                    return new Number(Left, LeftValue, rightSplit, null);
                }
            }
            else if (RightValue >= 10)
            {
                var rightSplit = new Number(null, RightValue.Value / 2, null, (RightValue.Value + 1) / 2);
                return new Number(Left, LeftValue, rightSplit, null);
            }

            return this;
        }

        public int Magnitude =>
            3 * (LeftValue ?? Left!.Magnitude) + 2 * (RightValue ?? Right!.Magnitude);

        public override string ToString() =>
            $"[{Left?.ToString() ?? LeftValue!.ToString()},{Right?.ToString() ?? RightValue!.ToString()}]";
    }
}
