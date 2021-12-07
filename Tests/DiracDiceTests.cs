using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.DiracDice.Tests;

[TestClass]
public class DiracDiceTests
{
    [TestMethod]
    [DataRow("Player 1 starting position: 4\r\nPlayer 2 starting position: 8", 739785)]
    public void GetLosingScoreTimesDieRolls_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var diracDice = new DiracDice(reader);

        // Act
        int result = diracDice.GetLosingScoreTimesDieRolls();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
