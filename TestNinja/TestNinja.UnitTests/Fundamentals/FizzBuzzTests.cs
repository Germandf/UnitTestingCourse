using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class FizzBuzzTests
{
    [Test]
    [TestCase(15, "FizzBuzz", TestName = "GetOutput_InputIsDivisibleBy3And5_ReturnFizzBuzz")]
    [TestCase(3, "Fizz", TestName = "GetOutput_InputIsDivisibleBy3Only_ReturnFizz")]
    [TestCase(5, "Buzz", TestName = "GetOutput_InputIsDivisibleBy5Only_ReturnBuzz")]
    [TestCase(1, "1", TestName = "GetOutput_InputIsNotDivisibleBy3Or5_ReturnTheSameNumber")]
    public void GetOutput_WhenCalled_ReturnExpectedResult(int input, string expectedOutput)
    {
        var output = FizzBuzz.GetOutput(input);

        Assert.AreEqual(expectedOutput, output);
    }
}
