using YoutubeToMp3.Application.Utils;

namespace YoutubeToMp3.UnityTests.Utils
{
    public sealed class StringUtilsTest
    {

        [Theory]
        [InlineData("test", "test")]
        [InlineData("|test|", "test")]
        [InlineData("*test*", "test")]
        [InlineData("/test/", "test")]
        [InlineData(@"\test\", "test")]
        [InlineData("<test>", "test")]
        [InlineData("test?", "test")]
        public void RemoveInvalidCharacters_ShouldReturnCleanString(string text, string expected)
        {
            var actual = StringUtils.RemoveInvalidCharacters(text);

            Assert.Equal(expected, actual);
        }
    }
}
