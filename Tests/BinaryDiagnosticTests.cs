using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.BinaryDiagnostic.Tests;

[TestClass]
public class BinaryDiagnosticTests
{
    [TestMethod]
    [DataRow("00100", 4, 27)]
    [DataRow("00100\r\n11110\r\n10110\r\n10111\r\n10101\r\n01111\r\n00111\r\n11100\r\n10000\r\n11001\r\n00010\r\n01010", 22, 9)]
    public void GetGammaAndEpsilon_Always_ReturnsExpectedResult(string input, int expectedGamma, int expectedEpsilon)
    {
        // Arrange
        var reader = new StringReader(input);
        var binaryDiagnostic = new BinaryDiagnostic(reader);

        // Act
        (int Gamma, int Epsilon) result = binaryDiagnostic.GetGammaAndEpsilon();

        // Assert
        Assert.AreEqual(expectedGamma, result.Gamma);
        Assert.AreEqual(expectedEpsilon, result.Epsilon);
    }
}
