namespace YoutubeToMp3.Application.Utils
{
    public static class FolderUtils
    {
        private const string DownloadBaseDirectory = "Downloads";
        private const string FfmpegPathName = "ffmpeg.exe";
        private const string DefaultFileExtension = ".mp3";

        public static void CreateDirectoryIfNotExits(string name)
        {
            string safeFolderName = StringUtils.RemoveInvalidCharacters(name);
            string path = Path.Combine(DownloadBaseDirectory, safeFolderName);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static string GetOutputPath(string outputPathFile, string videoTitle) =>
            string.IsNullOrEmpty(outputPathFile) ?
                PathBuider(videoTitle) :
                PathBuider(outputPathFile, videoTitle);

        public static string PathBuider(string videoTitle)
        {
            string safeVideoTitle = StringUtils.RemoveInvalidCharacters(videoTitle);

            return Path.Combine(DownloadBaseDirectory, string.Concat(safeVideoTitle, DefaultFileExtension));
        }

        public static string PathBuider(string playlistTitle, string videoTitle)
        {
            string safePlaylistTitle = StringUtils.RemoveInvalidCharacters(playlistTitle);
            string safeVideoTitle = StringUtils.RemoveInvalidCharacters(videoTitle);

            return Path.Combine(DownloadBaseDirectory, safePlaylistTitle, string.Concat(safeVideoTitle, DefaultFileExtension));
        }

        public static string GetFfmpegPath() =>
            $@"{Directory.GetCurrentDirectory()}\{FfmpegPathName}";
    }
}
