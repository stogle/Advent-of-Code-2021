using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.SyntaxScoring.Tests;

[TestClass]
public class SyntaxScoringTests
{
    [TestMethod]
    [DataRow("[({(<(())[]>[[{[]{<()<>>", 0)]
    [DataRow("{([(<{}[<>[]}>{[]{[(<()>", 1197)]
    [DataRow("[[<[([]))<([[{}[[()]]]", 3)]
    [DataRow("[{[{({}]{}}([{[{{{}}([]", 57)]
    [DataRow("<{([([[(<>()){}]>(<<{{", 25137)]
    [DataRow("[({(<(())[]>[[{[]{<()<>>\r\n[(()[<>])]({[<{<<[]>>(\r\n{([(<{}[<>[]}>{[]{[(<()>\r\n(((({<>}<{<{<>}{[]{[]{}\r\n[[<[([]))<([[{}[[()]]]\r\n[{[{({}]{}}([{[{{{}}([]\r\n{<[[]]>}<{[{[{[]{()[[[]\r\n[<(<(<(<{}))><([]([]()\r\n<{([([[(<>()){}]>(<<{{\r\n<{([{{}}[<[[[<>{}]]]>[]]", 26397)]
    public void GetTotalSyntaxErrorScore_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var syntaxScoring = new SyntaxScoring(reader);

        // Act
        int result = syntaxScoring.GetTotalSyntaxErrorScore();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
