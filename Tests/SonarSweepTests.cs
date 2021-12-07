using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.SonarSweep.Tests;

[TestClass]
public class SonarSweepTests
{
    [TestMethod]
    [DataRow("", 0)]
    [DataRow("1", 0)]
    [DataRow("199\r\n200\r\n208\r\n210\r\n200\r\n207\r\n240\r\n269\r\n260\r\n263", 7)]
    public void GetNumberOfIncreases_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var sonarSweep = new SonarSweep(reader);

        // Act
        int result = sonarSweep.GetNumberOfIncreases();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
