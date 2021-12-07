using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.Amphipod.Tests;

[TestClass]
public class AmphipodTests
{
    [TestMethod]
    [DataRow("#############\r\n#...........#\r\n###B#C#B#D###\r\n  #A#D#C#A#\r\n  #########", 12521)]
    public void GetMinimumEnergy_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var amphipod = new Amphipod(reader);

        // Act
        int result = amphipod.GetMinimumEnergy();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
