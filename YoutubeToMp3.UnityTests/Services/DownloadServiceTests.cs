using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;
using YoutubeToMp3.Application.Services;

namespace YoutubeToMp3.UnityTests.Services
{
    public class DownloadServiceTests
    {
        private readonly AutoMocker _mocker;
        private readonly Mock<IDownloadService> _mockDownloadService;
        private readonly Mock<ILogger<DownloadService>> _mockLogger;
        private readonly Mock<YoutubeClient> _mockYoutube;

        public DownloadServiceTests()
        {
            _mocker = new AutoMocker();
            _mockDownloadService = _mocker.GetMock<IDownloadService>();
            _mockLogger = _mocker.GetMock<ILogger<DownloadService>>();
            _mockYoutube = _mocker.GetMock<YoutubeClient>();
        }

        //[Fact]
        //public Task DownloadAudioAsync_ShouldReturnSuccess()
        //{
        //}

        [Fact]
        public async Task DownloadPlaylistAsync_ShouldReturnSuccess()
        {

            // Arrange
            Fixture fixture = new Fixture();
            var playlistFixture = new Playlist("PLVTLbc6i-h_iuhdwUfuPDLFLXG2QQnz-x", "teste", null, "teste", new List<Thumbnail>());
            var playlistVideoFixture = fixture.Create<IReadOnlyList<PlaylistVideo>>();

            _mockYoutube.Setup(y => y.Playlists.GetAsync(It.IsAny<string>(), default)).Returns(new ValueTask<Playlist>(playlistFixture));
            _mockYoutube.Setup(y => y.Playlists.GetVideosAsync(It.IsAny<string>(), default).CollectAsync()).Returns(new ValueTask<IReadOnlyList<PlaylistVideo>>(playlistVideoFixture));

            var downloadService = new DownloadService(_mockLogger.Object);

            // Act
            await downloadService.DownloadPlaylistAsync("www.google.com.br");

            // Assert
            _mockYoutube.Verify(y => y.Playlists.GetAsync("", default), Times.Once);
        }
    }
}
