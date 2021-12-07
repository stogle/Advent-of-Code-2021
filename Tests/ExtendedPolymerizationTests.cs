using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.ExtendedPolymerization.Tests;

[TestClass]
public class ExtendedPolymerizationTests
{
    [TestMethod]
    [DataRow("NNCB\r\n\r\nCH -> B\r\nHH -> N\r\nCB -> H\r\nNH -> C\r\nHB -> C\r\nHC -> B\r\nHN -> C\r\nNN -> C\r\nBH -> H\r\nNC -> B\r\nNB -> B\r\nBN -> B\r\nBB -> N\r\nBC -> B\r\nCC -> N\r\nCN -> C", 10, 1749, 161)]
    public void GetMostCommonAndLeastCommonQuantities_Always_ReturnsExpectedResult(string input, int steps, int expectedMostCommonQuantity, int expectedLeastCommonQuantity)
    {
        // Arrange
        var reader = new StringReader(input);
        var extendedPolymerization = new ExtendedPolymerization(reader);

        // Act
        (int MostCommonQuantity, int LeastCommonQuantity) result = extendedPolymerization.GetMostCommonAndLeastCommonQuantities(steps);

        // Assert
        Assert.AreEqual(expectedMostCommonQuantity, result.MostCommonQuantity);
        Assert.AreEqual(expectedLeastCommonQuantity, result.LeastCommonQuantity);
    }
}
