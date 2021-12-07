using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.PassagePathing.Tests;

[TestClass]
public class PassagePathingTests
{
    [TestMethod]
    [DataRow("start-A\r\nstart-b\r\nA-c\r\nA-b\r\nb-d\r\nA-end\r\nb-end", 10)]
    [DataRow("dc-end\r\nHN-start\r\nstart-kj\r\ndc-start\r\ndc-HN\r\nLN-dc\r\nHN-end\r\nkj-sa\r\nkj-HN\r\nkj-dc", 19)]
    [DataRow("fs-end\r\nhe-DX\r\nfs-he\r\nstart-DX\r\npj-DX\r\nend-zg\r\nzg-sl\r\nzg-pj\r\npj-he\r\nRW-he\r\nfs-DX\r\npj-RW\r\nzg-RW\r\nstart-pj\r\nhe-WI\r\nzg-he\r\npj-fs\r\nstart-RW", 226)]
    public void GetTotalPaths_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var passagePathing = new PassagePathing(reader);

        // Act
        int result = passagePathing.GetTotalPaths();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
