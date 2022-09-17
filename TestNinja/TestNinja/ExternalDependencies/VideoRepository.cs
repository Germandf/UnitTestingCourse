using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TestNinja.ExternalDependencies;

public interface IVideoRepository
{
    List<Video> GetUnprocessedVideos();
}

public class VideoRepository : IVideoRepository
{
    public List<Video> GetUnprocessedVideos()
    {
        using var context = new VideoContext();

        var videos = context.Videos.Where(x => !x.IsProcessed).ToList();

        return videos;
    }
}

public class Video
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsProcessed { get; set; }
}

public class VideoContext : DbContext
{
    public DbSet<Video> Videos { get; set; }
}