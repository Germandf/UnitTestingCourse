using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class DemeritPointsCalculatorTests
{
    private DemeritPointsCalculator _demeritPointsCalculator = null!;

    [SetUp]
    public void SetUp()
    {
        _demeritPointsCalculator = new();
    }

    [Test]
    [TestCase(-1)]
    [TestCase(301)]
    public void CalculateDemeritPoints_SpeedIsOutOfRange_ThrowsArgumentOutOfRangeException(int speed)
    {
        Assert.That(() => _demeritPointsCalculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(64, 0)]
    [TestCase(65, 0)]
    [TestCase(66, 0)]
    [TestCase(70, 1)]
    [TestCase(75, 2)]
    public void CalculateDemeritPoints_WhenCalled_ReturnsDemeritPoints(int speed, int expectedOutput)
    {
        var output = _demeritPointsCalculator.CalculateDemeritPoints(speed);

        Assert.That(output, Is.EqualTo(expectedOutput));
    }
}
