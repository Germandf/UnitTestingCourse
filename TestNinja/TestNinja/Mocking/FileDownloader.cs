using System.Net;

namespace TestNinja.Mocking;

public interface IFileDownloader
{
    void DownloadFile(string url, string path);
}

public class FileDownloader : IFileDownloader
{
    public void DownloadFile(string url, string path)
    {
#pragma warning disable SYSLIB0014 // Type or member is obsolete
        var client = new WebClient();
#pragma warning restore SYSLIB0014 // Type or member is obsolete
        client.DownloadFile(url, path);
    }
}
