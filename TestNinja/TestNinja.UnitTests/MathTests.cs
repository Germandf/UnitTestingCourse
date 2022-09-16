using NUnit.Framework;
using System.Linq;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests;

[TestFixture]
public class MathTests
{
    private Math _math = null!;

    [SetUp]
    public void SetUp()
    {
        _math = new();
    }

    [Test]
    ///[Ignore("Because I wanted to!")]
    public void Add_WhenCalled_ReturnTheSumOfArguments()
    {
        var result = _math.Add(1, 2);

        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    [TestCase(2, 1, 2)]
    [TestCase(1, 2, 2)]
    [TestCase(1, 1, 1)]
    public void Max_WhenCalled_ReturnGreaterArgument(int a, int b, int expectedResult)
    {
        var result = _math.Max(a, b);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
    {
        var result = _math.GetOddNumbers(5);

        // General - First way
        Assert.That(result, Is.Not.Empty);

        // General - Second way
        Assert.That(result.Count(), Is.EqualTo(3));

        // Specific
        Assert.That(result, Does.Contain(1));
        Assert.That(result, Does.Contain(3));
        Assert.That(result, Does.Contain(5));

        // Specific - Refactor
        Assert.That(result, Is.EquivalentTo(new[] {1, 3, 5}));

        // Extras - Not necessary
        Assert.That(result, Is.Ordered);
        Assert.That(result, Is.Unique);
    }
}
