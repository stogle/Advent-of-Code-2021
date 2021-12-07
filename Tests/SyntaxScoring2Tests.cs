using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.SyntaxScoring.Tests;

[TestClass]
public class SyntaxScoring2Tests
{
    [TestMethod]
    [DataRow("[({(<(())[]>[[{[]{<()<>>", 288957L)]
    [DataRow("[(()[<>])]({[<{<<[]>>(", 5566L)]
    [DataRow("(((({<>}<{<{<>}{[]{[]{}", 1480781L)]
    [DataRow("{<[[]]>}<{[{[{[]{()[[[]", 995444L)]
    [DataRow("<{([{{}}[<[[[<>{}]]]>[]]", 294L)]
    [DataRow("[({(<(())[]>[[{[]{<()<>>\r\n[(()[<>])]({[<{<<[]>>(\r\n{([(<{}[<>[]}>{[]{[(<()>\r\n(((({<>}<{<{<>}{[]{[]{}\r\n[[<[([]))<([[{}[[()]]]\r\n[{[{({}]{}}([{[{{{}}([]\r\n{<[[]]>}<{[{[{[]{()[[[]\r\n[<(<(<(<{}))><([]([]()\r\n<{([([[(<>()){}]>(<<{{\r\n<{([{{}}[<[[[<>{}]]]>[]]", 288957L)]
    public void GetTotalSyntaxErrorScore_Always_ReturnsExpectedResult(string input, long expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var syntaxScoring = new SyntaxScoring2(reader);

        // Act
        long result = syntaxScoring.GetMiddleAutocompleteScore();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
