using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class PhoneNumberTests
{
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("")]
    public void Parse_NumberIsNullEmptyOrWhitespace_ThrowArgumentException(string? number)
    {
        Assert.That(() => PhoneNumber.Parse(number), Throws.ArgumentException);
    }

    [Test]
    public void Parse_NumberLengthIsNotEqualTo10_ThrowArgumentException()
    {
        Assert.That(() => PhoneNumber.Parse("123456789"), Throws.ArgumentException);
    }

    [Test]
    public void Parse_NumberLengthIsEqualTo10_ReturnPhoneNumber()
    {
        var phoneNumber = PhoneNumber.Parse("1234567890");

        Assert.That(phoneNumber, Is.Not.Null);
        Assert.That(phoneNumber.Area, Is.EqualTo("123"));
        Assert.That(phoneNumber.Major, Is.EqualTo("456"));
        Assert.That(phoneNumber.Minor, Is.EqualTo("7890"));
    }

    [Test]
    public void ToString_WhenCalled_ReturnFormattedNumber()
    {
        var phoneNumber = PhoneNumber.Parse("1234567890");

        var formattedNumber = phoneNumber.ToString();

        Assert.That(formattedNumber, Is.EqualTo("(123)456-7890"));
    }
}
