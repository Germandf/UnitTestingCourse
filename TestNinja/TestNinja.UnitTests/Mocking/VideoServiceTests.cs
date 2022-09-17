using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private Mock<IFileReader> _fileReader = null!;
    private Mock<IVideoRepository> _videoRepository = null!;
    private VideoService _videoService = null!;

    [SetUp]
    public void SetUp()
    {
        _fileReader = new Mock<IFileReader>();
        _videoRepository = new Mock<IVideoRepository>();
        _videoService = new VideoService(_fileReader.Object, _videoRepository.Object);
    }

    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnError()
    {
        _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

        var result = _videoService.ReadVideoTitle();

        Assert.That(result, Does.Contain("error").IgnoreCase);
    }

    [Test]
    public void ReadVideoTitle_ValidFile_ReturnTitle()
    {
        _fileReader.Setup(fr => fr.Read("video.txt")).Returns(@"{ Id: 1, Title: ""Foo"", IsProcessed: true  }");

        var result = _videoService.ReadVideoTitle();

        Assert.That(result, Does.Contain("Foo").IgnoreCase);
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnEmptyString()
    {
        _videoRepository.Setup(fr => fr.GetUnprocessedVideos()).Returns(
            new List<Video>());

        var result = _videoService.GetUnprocessedVideosAsCsv();

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_UnprocessedVideos_ReturnStringWithIds()
    {
        _videoRepository.Setup(fr => fr.GetUnprocessedVideos()).Returns(
            new List<Video>() { new() { Id = 1 }, new() { Id = 2 } });

        var result = _videoService.GetUnprocessedVideosAsCsv();

        Assert.That(result, Is.EqualTo("1,2"));
    }
}
