using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.TrenchMap.Tests;

[TestClass]
public class TrenchMap2Tests
{
    [TestMethod]
    [DataRow("..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#\r\n\r\n#..#.\r\n#....\r\n##..#\r\n..#..\r\n..###", 50, 3351)]
    public void GetEnhancedImage_Always_ReturnsExpectedResult(string input, int enhancementCount, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var trenchMap = new TrenchMap2(reader);

        // Act
        TrenchMap2.Image result = trenchMap.GetEnhancedImage(enhancementCount);

        // Assert
        Assert.AreEqual(expectedResult, result.GetLitPixelCount());
    }
}
