namespace YoutubeToMp3.Application.Utils
{
    public static class StringUtils
    {
        public static string RemoveInvalidCharacters(string path)
        {
            if (path.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                foreach (char c in Path.GetInvalidFileNameChars())
                {
                    path = path.Replace(c.ToString(), string.Empty);
                }
            }
            return path;
        }
    }
}
