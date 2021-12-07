using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.Dive.Tests;

[TestClass]
public class Dive2Tests
{
    [TestMethod]
    [DataRow("", 0, 0, 0)]
    [DataRow("forward 1", 1, 0, 0)]
    [DataRow("down 1", 0, 1, 0)]
    [DataRow("up 1", 0, -1, 0)]
    [DataRow("forward 5\r\ndown 5\r\nforward 8\r\nup 3\r\ndown 8\r\nforward 2", 15, 10, 60)]
    public void GetHorizontalAndAimAndDepth_Always_ReturnsExpectedResult(string input, int expectedHorizontal, int expectedAim, int expectedVertical)
    {
        // Arrange
        var reader = new StringReader(input);
        var dive = new Dive2(reader);

        // Act
        (int Horizontal, int Aim, int Vertical) result = dive.GetHorizontalAndAimAndAndDepth();

        // Assert
        Assert.AreEqual(expectedHorizontal, result.Horizontal);
        Assert.AreEqual(expectedAim, result.Aim);
        Assert.AreEqual(expectedVertical, result.Vertical);
    }
}
