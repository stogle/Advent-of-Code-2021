using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.TransparentOrigami.Tests;

[TestClass]
public class TransparentOrigamiTests
{
    [TestMethod]
    [DataRow("6,10\r\n0,14\r\n9,10\r\n0,3\r\n10,4\r\n4,11\r\n6,0\r\n6,12\r\n4,1\r\n0,13\r\n10,12\r\n3,4\r\n3,0\r\n8,4\r\n1,10\r\n2,14\r\n8,10\r\n9,0\r\n\r\nfold along y=7\r\nfold along x=5", 17)]
    public void GetDotCounts_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var transparentOrigami = new TransparentOrigami(reader);

        // Act
        int result = transparentOrigami.GetDotCounts();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
