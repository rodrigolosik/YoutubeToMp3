using Microsoft.Extensions.Logging;
using System.Diagnostics;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;
using YoutubeToMp3.Application.Utils;

namespace YoutubeToMp3.Application.Services
{
    public sealed class DownloadService : IDownloadService
    {
        private readonly ILogger<DownloadService> _logger;
        private readonly YoutubeClient _youtube;

        public DownloadService(ILogger<DownloadService> logger)
        {
            _logger = logger;
            _youtube = new YoutubeClient();
        }

        public async Task DownloadAudioAsync(string url, string outputPathFile = "")
        {
            try
            {
                var video = await _youtube.Videos.GetAsync(url);
                var streamManifest = await _youtube.Videos.Streams.GetManifestAsync(video.Id);
                var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                var streamInfos = new IStreamInfo[] { audioStreamInfo };

                var outputPath = FolderUtils.GetOutputPath(outputPathFile, video.Title);
                var ffmpegPath = FolderUtils.GetFfmpegPath();

                _logger.LogInformation($"Download of video: {video.Title} started");
                Stopwatch stopwatch = Stopwatch.StartNew();

                await _youtube.Videos.DownloadAsync(streamInfos,
                    new ConversionRequestBuilder(outputPath)
                    .SetContainer(Container.Mp3)
                    .SetFFmpegPath(ffmpegPath)
                    .SetPreset(ConversionPreset.UltraFast)
                    .Build());

                stopwatch.Stop();
                _logger.LogInformation($@"Download of video {video.Title} finished after: {stopwatch.Elapsed.TotalSeconds}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error trying to download video {0}", url);
            }
        }

        public async Task DownloadPlaylistAsync(string url)
        {
            try
            {
                var playlistInfos = await _youtube.Playlists.GetAsync(url);

                var playlist = await _youtube.Playlists
                    .GetVideosAsync(playlistInfos.Id)
                    .CollectAsync();

                FolderUtils.CreateDirectoryIfNotExits(playlistInfos.Title);

                Parallel.ForEach(playlist, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, video =>
                {
                    DownloadAudioAsync(video.Id, playlistInfos.Title).Wait();
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error trying to download playlist {0}", url);
            }
        }
    }
}
