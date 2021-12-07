using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdventOfCode2021.ReactorReboot.Tests;

[TestClass]
public class ReactorRebootTests
{
    [TestMethod]
    [DataRow("on x=10..12,y=10..12,z=10..12\r\non x=11..13,y=11..13,z=11..13\r\noff x=9..11,y=9..11,z=9..11\r\non x=10..10,y=10..10,z=10..10", 39)]
    [DataRow("on x=-20..26,y=-36..17,z=-47..7\r\non x=-20..33,y=-21..23,z=-26..28\r\non x=-22..28,y=-29..23,z=-38..16\r\non x=-46..7,y=-6..46,z=-50..-1\r\non x=-49..1,y=-3..46,z=-24..28\r\non x=2..47,y=-22..22,z=-23..27\r\non x=-27..23,y=-28..26,z=-21..29\r\non x=-39..5,y=-6..47,z=-3..44\r\non x=-30..21,y=-8..43,z=-13..34\r\non x=-22..26,y=-27..20,z=-29..19\r\noff x=-48..-32,y=26..41,z=-47..-37\r\non x=-12..35,y=6..50,z=-50..-2\r\noff x=-48..-32,y=-32..-16,z=-15..-5\r\non x=-18..26,y=-33..15,z=-7..46\r\noff x=-40..-22,y=-38..-28,z=23..41\r\non x=-16..35,y=-41..10,z=-47..6\r\noff x=-32..-23,y=11..30,z=-14..3\r\non x=-49..-5,y=-3..45,z=-29..18\r\noff x=18..30,y=-20..-8,z=-3..13\r\non x=-41..9,y=-7..43,z=-33..15\r\non x=-54112..-39298,y=-85059..-49293,z=-27449..7877\r\non x=967..23432,y=45373..81175,z=27513..53682", 590784)]
    public void GetTotalCubesOn_Always_ReturnsExpectedResult(string input, int expectedResult)
    {
        // Arrange
        var reader = new StringReader(input);
        var reactorReboot = new ReactorReboot(reader);

        // Act
        int result = reactorReboot.GetTotalCubesOn();

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}
