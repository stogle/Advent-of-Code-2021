using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.BinaryDiagnostic.Tests;

[TestClass]
public class BinaryDiagnostic2Tests
{
    [TestMethod]
    [DataRow("00100\r\n11110", 4, 30)]
    [DataRow("00100\r\n11110\r\n10110\r\n10111\r\n10101\r\n01111\r\n00111\r\n11100\r\n10000\r\n11001\r\n00010\r\n01010", 23, 10)]
    public void GetO2GeneratorAndCO2ScrubberRatings_Always_ReturnsExpectedResult(string input, int expectedGamma, int expectedEpsilon)
    {
        // Arrange
        var reader = new StringReader(input);
        var binaryDiagnostic = new BinaryDiagnostic2(reader);

        // Act
        (int Gamma, int Epsilon) result = binaryDiagnostic.GetO2GeneratorAndCO2ScrubberRatings();

        // Assert
        Assert.AreEqual(expectedGamma, result.Gamma);
        Assert.AreEqual(expectedEpsilon, result.Epsilon);
    }
}
