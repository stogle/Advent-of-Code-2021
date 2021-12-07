using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.Dive.Tests;

[TestClass]
public class DiveTests
{
    [TestMethod]
    [DataRow("", 0, 0)]
    [DataRow("forward 1", 1, 0)]
    [DataRow("down 1", 0, 1)]
    [DataRow("up 1", 0, -1)]
    [DataRow("forward 5\r\ndown 5\r\nforward 8\r\nup 3\r\ndown 8\r\nforward 2", 15, 10)]
    public void GetHorizontalAndDepth_Always_ReturnsExpectedResult(string input, int expectedHorizontal, int expectedVertical)
    {
        // Arrange
        var reader = new StringReader(input);
        var dive = new Dive(reader);

        // Act
        (int Horizontal, int Vertical) result = dive.GetHorizontalAndDepth();

        // Assert
        Assert.AreEqual(expectedHorizontal, result.Horizontal);
        Assert.AreEqual(expectedVertical, result.Vertical);
    }
}
