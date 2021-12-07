﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.Snailfish.Tests;

[TestClass]
public class SnailfishTests
{
    [TestMethod]
    [DataRow("[9,1]", "[9,1]", 29)]
    [DataRow("[1,9]", "[1,9]", 21)]
    [DataRow("[[9,1],[1,9]]", "[[9,1],[1,9]]", 129)]
    [DataRow("[[1,2],[[3,4],5]]", "[[1,2],[[3,4],5]]", 143)]
    [DataRow("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384)]
    [DataRow("[[[[1,1],[2,2]],[3,3]],[4,4]]", "[[[[1,1],[2,2]],[3,3]],[4,4]]", 445)]
    [DataRow("[[[[3,0],[5,3]],[4,4]],[5,5]]", "[[[[3,0],[5,3]],[4,4]],[5,5]]", 791)]
    [DataRow("[[[[5,0],[7,4]],[5,5]],[6,6]]", "[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137)]
    [DataRow("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]
    [DataRow("[1,1]\r\n[2,2]\r\n[3,3]\r\n[4,4]", "[[[[1,1],[2,2]],[3,3]],[4,4]]", 445)]
    [DataRow("[1,1]\r\n[2,2]\r\n[3,3]\r\n[4,4]\r\n[5,5]", "[[[[3,0],[5,3]],[4,4]],[5,5]]", 791)]
    [DataRow("[1,1]\r\n[2,2]\r\n[3,3]\r\n[4,4]\r\n[5,5]\r\n[6,6]", "[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137)]
    [DataRow("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]\r\n[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]\r\n[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]\r\n[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]\r\n[7,[5,[[3,8],[1,4]]]]\r\n[[2,[2,2]],[8,[8,1]]]\r\n[2,9]\r\n[1,[[[9,3],9],[[9,0],[0,7]]]]\r\n[[[5,[7,4]],7],1]\r\n[[[[4,2],2],6],[8,7]]", "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]
    [DataRow("[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]\r\n[[[5,[2,8]],4],[5,[[9,9],0]]]\r\n[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]\r\n[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]\r\n[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]\r\n[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]\r\n[[[[5,4],[7,7]],8],[[8,3],8]]\r\n[[9,3],[[9,9],[6,[4,9]]]]\r\n[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]\r\n[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]", "[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]", 4140)]
    public void GetSum_Always_ReturnsExpectedResult(string input, string expectedString, int expectedMagnitude)
    {
        // Arrange
        var reader = new StringReader(input);
        var snailfish = new Snailfish(reader);

        // Act
        Snailfish.Number result = snailfish.GetSum();

        // Assert
        Assert.AreEqual(expectedString, result.ToString());
        Assert.AreEqual(expectedMagnitude, result.Magnitude);
    }
}
