﻿using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class ErrorLoggerTests
{
    private ErrorLogger _logger = null!;

    [SetUp]
    public void SetUp()
    {
        _logger = new();
    }

    [Test]
    public void Log_WhenCalled_SetTheLastErrorProperty()
    {
        _logger.Log("a");

        Assert.That(_logger.LastError, Is.EqualTo("a"));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Log_InvalidError_ThrowArgumentNullException(string error)
    {
        Assert.That(() => _logger.Log(error), Throws.ArgumentNullException);
    }

    [Test]
    public void Log_ValidError_RaiseErrorLoggedEvent()
    {
        var id = Guid.Empty;
        _logger.ErrorLogged += (sender, args) => { id = args; };

        _logger.Log("a");

        Assert.That(id, Is.Not.EqualTo(Guid.Empty));
    }
}
