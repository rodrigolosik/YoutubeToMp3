using YoutubeToMp3.Application.Utils;

namespace YoutubeToMp3.UnityTests.Utils
{
    public sealed class FolderUtilsTest
    {
        [Fact]
        public void GetFfmpegPath_ShouldReturnString()
        {
            var ffmpegPath = FolderUtils.GetFfmpegPath();

            Assert.NotNull(ffmpegPath);
            Assert.NotEqual(string.Empty, ffmpegPath);
        }

        [Theory]
        [InlineData("", "music")]
        [InlineData("playlist", "music")]
        public void GetOutputPath_ShouldReturnString (string outputPathFile, string videoTitle)
        {
            var outputPath = FolderUtils.GetOutputPath(outputPathFile, videoTitle);

            Assert.NotNull(outputPath);
            Assert.NotEqual(string.Empty, outputPath);
            Assert.EndsWith("mp3", outputPath);
        }
    }
}
