namespace AdventOfCode2021.ArithmeticLogicUnit;

public class ArithmeticLogicUnit2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var arithmeticLogicUnit = new ArithmeticLogicUnit2(reader);
        string? result = arithmeticLogicUnit.GetLargestModelNumber();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public ArithmeticLogicUnit2(TextReader reader)
    {
        _reader = reader;
    }

    public string? GetLargestModelNumber()
    {
        (int A, int B, int C)[] variables = new (int, int, int)[14];
        for (int i = 0; i < variables.Length; i++)
        {
            ThrowIfNotEqual("inp w", _reader.ReadLine());
            ThrowIfNotEqual("mul x 0", _reader.ReadLine());
            ThrowIfNotEqual("add x z", _reader.ReadLine());
            ThrowIfNotEqual("mod x 26", _reader.ReadLine());

            string? line = _reader.ReadLine();
            int a = int.Parse(line!.Split(' ')[2]);
            ThrowIfNotEqual($"div z {a}", line);

            line = _reader.ReadLine();
            int b = int.Parse(line!.Split(' ')[2]);
            ThrowIfNotEqual($"add x {b}", line);

            ThrowIfNotEqual("eql x w", _reader.ReadLine());
            ThrowIfNotEqual("eql x 0", _reader.ReadLine());
            ThrowIfNotEqual("mul y 0", _reader.ReadLine());
            ThrowIfNotEqual("add y 25", _reader.ReadLine());
            ThrowIfNotEqual("mul y x", _reader.ReadLine());
            ThrowIfNotEqual("add y 1", _reader.ReadLine());
            ThrowIfNotEqual("mul z y", _reader.ReadLine());
            ThrowIfNotEqual("mul y 0", _reader.ReadLine());
            ThrowIfNotEqual("add y w", _reader.ReadLine());

            line = _reader.ReadLine();
            int c = int.Parse(line!.Split(' ')[2]);
            ThrowIfNotEqual($"add y {c}", line);

            ThrowIfNotEqual("mul y x", _reader.ReadLine());
            ThrowIfNotEqual("add z y", _reader.ReadLine());

            variables[i] = (a, b, c);
        }

        var result = new char[variables.Length];
        return FindDigit(variables, result) ? new string(result) : null;
    }

    private bool FindDigit((int A, int B, int C)[] variables, char[] result, int index = 0, int z = 0)
    {
        if (index == variables.Length)
        {
            return true;
        }

        (int a, int b, int c) = variables[index];
        var range = Enumerable.Range(1, 9);
        if (a == 26)
        {
            range = range.Where(n => z % 26 + b == n);
        }
        foreach (var digit in range)
        {
            result[index] = digit.ToString()[0];
            if (FindDigit(variables, result, index + 1, a == 1 ? z * 26 + digit + c : z / 26))
            {
                return true;
            }
        }

        return false;
    }

    private void ThrowIfNotEqual(string expected, string? actual)
    {
        if (actual != expected)
        {
            throw new InvalidOperationException($"Expected '{expected}' but read '{actual}");
        }
    }
}
