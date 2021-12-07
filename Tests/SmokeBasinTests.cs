using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.SmokeBasin.Tests;

[TestClass]
public class SmokeBasinTests
{
    [TestMethod]
    [DataRow("2199943210\r\n", 3)]
    [DataRow("2199943210\r\n3987894921\r\n9856789892\r\n8767896789\r\n9899965678\r\n", 15)]
    public void GetTotalRiskLevels_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var smokeBasin = new SmokeBasin(reader);

        // Act
        int result = smokeBasin.GetTotalRiskLevels();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
