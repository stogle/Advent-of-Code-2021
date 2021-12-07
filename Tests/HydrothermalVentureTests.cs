using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.HydrothermalVenture.Tests;

[TestClass]
public class HydrothermalVentureTests
{
    [TestMethod]
    [DataRow("", 0)]
    [DataRow("0,9 -> 5,9\r\n8, 0 -> 0, 8\r\n9, 4 -> 3, 4\r\n2, 2 -> 2, 1\r\n7, 0 -> 7, 4\r\n6, 4 -> 2, 0\r\n0, 9 -> 2, 9\r\n3, 4 -> 1, 4\r\n0, 0 -> 8, 8\r\n5, 5 -> 8, 2", 5)]
    public void GetDangerousPointCount_Always_ReturnsExpectedResult(string input, int? expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var hydrothermalVenture = new HydrothermalVenture(reader);

        // Act
        int result = hydrothermalVenture.GetDangerousPointCount();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
