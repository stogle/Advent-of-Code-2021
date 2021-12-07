using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.TreacheryOfWhales.Tests;

[TestClass]
public class TreacheryOfWhalesTests
{
    [TestMethod]
    [DataRow("1", 0)]
    [DataRow("1,2", 1)]
    [DataRow("1,2,3", 2)]
    [DataRow("16,1,2,0,4,2,7,1,2,14", 37)]
    public void GetFuel_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var treacheryOfWhatles = new TreacheryOfWhales(reader);

        // Act
        int result = treacheryOfWhatles.GetFuel();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
