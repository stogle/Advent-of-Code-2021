using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.Snailfish.Tests;

[TestClass]
public class Snailfish2Tests
{
    [TestMethod]
    [DataRow("[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]\r\n[[[5,[2,8]],4],[5,[[9,9],0]]]\r\n[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]\r\n[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]\r\n[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]\r\n[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]\r\n[[[[5,4],[7,7]],8],[[8,3],8]]\r\n[[9,3],[[9,9],[6,[4,9]]]]\r\n[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]\r\n[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]", 3993)]
    public void GetMaxMagnitudeOfSumOfPairs_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var snailfish = new Snailfish2(reader);

        // Act
        int result = snailfish.GetMaxMagnitudeOfSumOfPairs();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
