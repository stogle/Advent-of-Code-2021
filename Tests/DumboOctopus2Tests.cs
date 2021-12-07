using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.DumboOctopus.Tests
{
    [TestClass]
    public class DumboOctopus2Tests
    {
        [TestMethod]
        [DataRow("11111\r\n19991\r\n19191\r\n19991\r\n11111", 6)]
        [DataRow("5483143223\r\n2745854711\r\n5264556173\r\n6141336146\r\n6357385478\r\n4167524645\r\n2176841721\r\n6882881134\r\n4846848554\r\n5283751526", 195)]
        public void GetGammaAndEpsilon_Always_ReturnsExpectedResult(string input, int expectedResult)
        {
            // Arrange
            var reader = new StringReader(input);
            var dumboOctopus = new DumboOctopus2(reader);

            // Act
            int result = dumboOctopus.GetFirstStepWhereAllFlash();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}