using Microsoft.AspNetCore.Mvc;
using YoutubeToMp3.Application.Services;

namespace YoutubeToMp3.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly ILogger<DownloadController> _logger;
        private readonly IDownloadService _downloadService;

        public DownloadController(ILogger<DownloadController> logger, IDownloadService downloadService)
        {
            _downloadService = downloadService;
            _logger = logger;
        }

        [HttpPost("audio")]
        public async Task<IActionResult> DownloadAudio([FromBody] string url)
        {
            try
            {
                await _downloadService.DownloadAudioAsync(url);
                return Ok(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error trying to process your request. Please try again later.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("playlist")]
        public async Task<IActionResult> DownloadPlaylist([FromBody] string url)
        {
            try
            {
                await _downloadService.DownloadPlaylistAsync(url);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error trying to process your request. Please try again later.");
                return BadRequest(ex);
            }
        }
    }
}
