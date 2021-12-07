using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.PacketDecoder.Tests;

[TestClass]
public class PacketDecoder2Tests
{
    [TestMethod]
    [DataRow("C200B40A82", 3L)] // Sum (1, 2)
    [DataRow("04005AC33890", 54L)] // Product (6, 9)
    [DataRow("880086C3E88112", 7L)] // Minimum (7, 8, 9)
    [DataRow("D8005AC2A8F0", 1L)] // Less Than (5, 15)
    [DataRow("F600BC2D8F", 0L)] // Greater Than (5, 15)
    [DataRow("9C005AC2F8F0", 0L)] // Equal (5, 15)
    [DataRow("9C0141080250320F1802104A08", 1L)] // Equals (Sum (1, 3), Product (2, 2))
    public void GetValue_Always_ReturnsExpectedResult(string input, long expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var packetDecoder = new PacketDecoder2(reader);

        // Act
        long result = packetDecoder.GetValue();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
