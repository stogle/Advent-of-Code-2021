using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.Chiton.Tests;

[TestClass]
public class Chiton2Tests
{
    [TestMethod]
    [DataRow("1163751742\r\n1381373672\r\n2136511328\r\n3694931569\r\n7463417111\r\n1319128137\r\n1359912421\r\n3125421639\r\n1293138521\r\n2311944581", 315)]
    public void GetLowestTotalRisk_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var chiton = new Chiton2(reader);

        // Act
        int result = chiton.GetLowestTotalRisk();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
