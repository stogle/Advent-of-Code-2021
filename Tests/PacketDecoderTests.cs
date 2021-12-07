using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.PacketDecoder.Tests;

[TestClass]
public class PacketDecoderTests
{
    [TestMethod]
    [DataRow("D2FE28", 6)] // Literal 2021
    [DataRow("38006F45291200", 9)] // Operator (Literal 10, Literal 20)
    [DataRow("EE00D40C823060", 14)] // Operator (Literal 1, Literal 2, Literal 3)
    [DataRow("8A004A801A8002F478", 16)] // Operator (Operator (Operator (Literal 16)))
    [DataRow("620080001611562C8802118E34", 12)] // Operator (Operator (Literal, Literal), Operator (Literal, Literal))
    [DataRow("C0015000016115A2E0802F182340", 23)] // Operator (Operator (Literal, Literal), Operator (Literal, Literal))
    [DataRow("A0016C880162017C3686B18A3D4780", 31)] // Operator (Operator (Literal, Literal, Literal, Literal, Literal))
    public void GetTotalPacketVersionNumbers_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var packetDecoder = new PacketDecoder(reader);

        // Act
        int result = packetDecoder.GetTotalPacketVersionNumbers();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
