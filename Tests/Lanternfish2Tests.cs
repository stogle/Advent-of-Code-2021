using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.Lanternfish.Tests;

[TestClass]
public class Lanternfish2Tests
{
    [TestMethod]
    [DataRow("3", 4, 2)]
    [DataRow("3", 13, 4)]
    [DataRow("3,4,3,1,2", 80, 5934)]
    [DataRow("3,4,3,1,2", 256, 26984457539)]
    public void GetDangerousPointCount_Always_ReturnsExpectedResult(string input, int days, long expectedTotal)
    {
        // Arrange
        var reader = new StringReader(input);
        var lanternfish = new Lanternfish2(reader);

        // Act
        long total = lanternfish.GetTotal(days);

        // Assert
        Assert.AreEqual(expectedTotal, total);
    }
}
