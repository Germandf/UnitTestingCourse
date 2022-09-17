using Moq;
using NUnit.Framework;
using System.Net;
using TestNinja.ExternalDependencies;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class InstallerHelperTests
{
    private Mock<IFileDownloader> _fileDownloader = null!;
    private InstallerHelper _installerHelper = null!;

    [SetUp]
    public void SetUp()
    {
        _fileDownloader = new Mock<IFileDownloader>();
        _installerHelper = new InstallerHelper(_fileDownloader.Object);
    }

    [Test]
    public void DownloadInstaller_DownloadFails_ReturnFalse()
    {
        _fileDownloader.Setup(x => x.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
            .Throws<WebException>();

        var result = _installerHelper.DownloadInstaller("customer", "installer");

        Assert.That(result, Is.False);
    }

    [Test]
    public void DownloadInstaller_DownloadCompletes_ReturnTrue()
    {
        var result = _installerHelper.DownloadInstaller("", "");

        Assert.That(result, Is.True);
    }
}
