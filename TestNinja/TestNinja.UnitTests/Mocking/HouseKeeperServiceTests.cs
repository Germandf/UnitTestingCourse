using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.ExternalDependencies;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class HouseKeeperServiceTests
{
    private Mock<IStatementGenerator> _statementGenerator = null!;
    private Mock<IEmailSender> _emailSender = null!;
    private Mock<IUnitOfWork> _unitOfWork = null!;
    private Mock<IMessageBox> _messageBox = null!;
    private HouseKeeperService _service = null!;
    private DateTime _statementDate = new DateTime(2017, 1, 1);
    private HouseKeeper _houseKeeper = null!;
    private string _statementFileName = null!;

    [SetUp]
    public void SetUp()
    {
        _houseKeeper = new HouseKeeper() { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

        _unitOfWork = new Mock<IUnitOfWork>();
        _unitOfWork.Setup(x => x.Query<HouseKeeper>()).Returns(new List<HouseKeeper>() { _houseKeeper }.AsQueryable());

        _statementFileName = "fileName";
        _statementGenerator = new Mock<IStatementGenerator>();
        _statementGenerator
            .Setup(x => x.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
            .Returns(() => _statementFileName);

        _emailSender = new Mock<IEmailSender>();

        _messageBox = new Mock<IMessageBox>();

        _service = new HouseKeeperService(
            _unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _messageBox.Object);
    }

    [Test]
    public void SendStatementEmails_WhenCalled_GenerateStatements()
    {
        _service.SendStatementEmails(_statementDate);

        _statementGenerator.Verify(x => x.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void SendStatementEmails_HouseKeepersEmailIsNullEmptyOrWhitespace_ShouldNotGenerateStatements(string? email)
    {
        _houseKeeper.Email = email;

        _service.SendStatementEmails(_statementDate);

        _statementGenerator.Verify(x => x.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
    }

    [Test]
    public void SendStatementEmails_WhenCalled_EmailStatements()
    {
        _service.SendStatementEmails(_statementDate);
        
        VerifyEmailSent();
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void SendStatementEmails_StatementFileNameIsNullEmptyOrWhitespace_ShouldNotEmailStatements(string? statementFileName)
    {
        _statementFileName = statementFileName!;

        _service.SendStatementEmails(_statementDate);
        
        VerifyEmailNotSent();
    }

    [Test]
    public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
    {
        _emailSender
            .Setup(x => x.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws<Exception>();

        _service.SendStatementEmails(_statementDate);

        _messageBox.Verify(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
    }

    private void VerifyEmailSent()
    {
        _emailSender.Verify(x => x.EmailFile(
            _houseKeeper.Email,
            _houseKeeper.StatementEmailBody,
            _statementFileName,
            It.IsAny<string>()));
    }

    private void VerifyEmailNotSent()
    {
        _emailSender.Verify(x => x.EmailFile(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Never);
    }
}
