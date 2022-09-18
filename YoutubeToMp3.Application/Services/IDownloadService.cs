using YoutubeExplode.Videos;

namespace YoutubeToMp3.Application.Services
{
    public interface IDownloadService
    {
        Task DownloadAudioAsync(string url, string outputPathFile = "");

        Task DownloadPlaylistAsync(string url);
    }
}
