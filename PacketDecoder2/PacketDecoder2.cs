using System.Text;

namespace AdventOfCode2021.PacketDecoder;

public class PacketDecoder2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var packetDecoder = new PacketDecoder2(reader);
        long result = packetDecoder.GetValue();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public PacketDecoder2(TextReader reader)
    {
        _reader = reader;
    }

    public long GetValue()
    {
        string line = _reader.ReadLine()!;
        string bits = string.Join("", line.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

        int position = 0;
        return GetValue(bits, ref position);
    }

    private long GetValue(string bits, ref int position)
    {
        int version = ReadInt32(3, ref position); // VVV
        int typeID = ReadInt32(3, ref position); // TTT

        if (typeID == 4) // literal value
        {
            bool hasMoreBits;
            var value = new StringBuilder();
            do
            {
                hasMoreBits = ReadBool(ref position); // A
                value.Append(ReadBits(4, ref position)); // AAAA
            }
            while (hasMoreBits);
            return Convert.ToInt64(value.ToString(), 2);
        }

        // operator
        var args = new List<long>();
        int lengthTypeID = ReadInt32(1, ref position); // I
        switch (lengthTypeID)
        {
            case 0:
                int length = ReadInt32(15, ref position); // LLLLLLLLLLLLLLL
                int end = position + length;
                while (position < end)
                {
                    args.Add(GetValue(bits, ref position));
                }
                break;
            case 1:
                int subPacketCount = ReadInt32(11, ref position); // LLLLLLLLLLL
                for (int i = 0; i < subPacketCount; i++)
                {
                    args.Add(GetValue(bits, ref position));
                }
                break;
        }

        switch (typeID)
        {
            case 0: // sum
                return args.Sum();
            case 1: // product
                return args.Aggregate((a, b) => a * b);
            case 2: // minimum
                return args.Min();
            case 3: // maximum
                return args.Max();
            case 5: // greater than
                return args[0] > args[1] ? 1 : 0;
            case 6: // less than
                return args[0] < args[1] ? 1 : 0;
            case 7: // equal to
                return args[0] == args[1] ? 1 : 0;
            default:
                throw new InvalidOperationException();
        }

        string ReadBits(int length, ref int position)
        {
            string result = bits.Substring(position, length);
            position += length;
            return result;
        }

        int ReadInt32(int length, ref int position) => Convert.ToInt32(ReadBits(length, ref position), 2);

        bool ReadBool(ref int position) => ReadBits(1, ref position) == "1";
    }
}
