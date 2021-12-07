using System.Text;

namespace AdventOfCode2021.PacketDecoder;

public class PacketDecoder
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var packetDecoder = new PacketDecoder(reader);
        int result = packetDecoder.GetTotalPacketVersionNumbers();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public PacketDecoder(TextReader reader)
    {
        _reader = reader;
    }

    public int GetTotalPacketVersionNumbers()
    {
        string line = _reader.ReadLine()!;
        string bits = string.Join("", line.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

        int position = 0;
        return GetTotalPacketVersionNumbers(bits, ref position);
    }

    public int GetTotalPacketVersionNumbers(string bits, ref int position)
    {
        int version = ReadInt32(3, ref position); // VVV
        int typeID = ReadInt32(3, ref position); // TTT

        switch (typeID)
        {
            case 4: // literal value
                bool hasMoreBits;
                var value = new StringBuilder();
                do
                {
                    hasMoreBits = ReadBool(ref position); // A
                    value.Append(ReadBits(4, ref position)); // AAAA
                }
                while (hasMoreBits);
                break;
            default: // operator
                int lengthTypeID = ReadInt32(1, ref position); // I
                switch (lengthTypeID)
                {
                    case 0:
                        int length = ReadInt32(15, ref position); // LLLLLLLLLLLLLLL
                        int end = position + length;
                        while (position < end)
                        {
                            version += GetTotalPacketVersionNumbers(bits, ref position);
                        }
                        break;
                    case 1:
                        int subPacketCount = ReadInt32(11, ref position); // LLLLLLLLLLL
                        for (int i = 0; i < subPacketCount; i++)
                        {
                            version += GetTotalPacketVersionNumbers(bits, ref position);
                        }
                        break;
                }
                break;
        }

        return version;

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
