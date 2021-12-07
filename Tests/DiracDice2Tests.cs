using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.DiracDice.Tests;

[TestClass()]
public class DiracDice2Tests
{
    [TestMethod]
    [DataRow("Player 1 starting position: 4\r\nPlayer 2 starting position: 8", 444356092776315L)]
    public void GetLosingScoreTimesDieRolls_Always_ReturnsExpectedResult(string input, long expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var diracDice = new DiracDice2(reader);

        // Act
        long result = diracDice.GetMaxTotalUniversesWherePlayerWins();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
