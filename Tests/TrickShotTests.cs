using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.TrickShot.Tests;

[TestClass]
public class TrickShotTests
{
    [TestMethod]
    [DataRow("target area: x=20..30, y=-10..-5", 45)]
    public void GetMaxYPosition_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var trickShot = new TrickShot(reader);

        // Act
        int result = trickShot.GetMaxYPosition();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
