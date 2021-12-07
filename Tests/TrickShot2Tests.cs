using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.TrickShot.Tests;

[TestClass]
public class TrickShot2Tests
{
    [TestMethod]
    [DataRow("target area: x=20..30, y=-10..-5", 112)]
    public void GetTotalDistinctVelocities_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var trickShot = new TrickShot2(reader);

        // Act
        int result = trickShot.GetTotalDistinctVelocities();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
