using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class DateHelperTests
{
    [Test]
    public void FirstOfNextMonth_WhenCalled_ResetDay()
    {
        var dateTime = new DateTime(2017, 11, 1);

        var result = DateHelper.FirstOfNextMonth(dateTime);

        Assert.That(result.Day, Is.EqualTo(1));
    }

    [Test]
    public void FirstOfNextMonth_DateMonthIsDecember_AddOneYear()
    {
        var dateTime = new DateTime(2017, 12, 1);

        var result = DateHelper.FirstOfNextMonth(dateTime);

        Assert.That(result.Year, Is.EqualTo(dateTime.Year + 1));
    }

    [Test]
    public void FirstOfNextMonth_DateMonthIsDecember_ResetMonth()
    {
        var dateTime = new DateTime(2017, 12, 1);

        var result = DateHelper.FirstOfNextMonth(dateTime);

        Assert.That(result.Month, Is.EqualTo(1));
    }

    [Test]
    public void FirstOfNextMonth_DateMonthIsNotDecember_KeepSameYear()
    {
        var dateTime = new DateTime(2017, 11, 1);

        var result = DateHelper.FirstOfNextMonth(dateTime);

        Assert.That(result.Year, Is.EqualTo(dateTime.Year));
    }

    [Test]
    public void FirstOfNextMonth_DateMonthIsNotDecember_AddOneMonth()
    {
        var dateTime = new DateTime(2017, 11, 1);

        var result = DateHelper.FirstOfNextMonth(dateTime);

        Assert.That(result.Month, Is.EqualTo(dateTime.Month + 1));
    }
}
