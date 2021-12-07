using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.SeaCucumber.Tests;

[TestClass]
public class SeaCucumberTests
{
    [TestMethod]
    [DataRow("v...>>.vv>\r\n.vv>>.vv..\r\n>>.>v>...v\r\n>>v>>.>.v.\r\nv>v.vv.v..\r\n>.>>..v...\r\n.vv..>.>v.\r\nv.v..>>v.v\r\n....v..v.>", 58)]
    public void GetMinimumStepWithNoMoves_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var seaCucumber = new SeaCucumber(reader);

        // Act
        int result = seaCucumber.GetMinimumStepWithNoMoves();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
