using Microsoft.AspNetCore.Mvc;
using CacheSample.Domain.Tracks;

namespace CacheSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ITrackRepository trackRepository;

        public TracksController(ITrackRepository historyRepository)
        {
            this.trackRepository = historyRepository;
        }

        [HttpGet]
        public IActionResult All()
        {
            var tracks = trackRepository.FindAll();
            return Ok(tracks);
        }

        [HttpGet("albums/{albumId}")]
        public IActionResult AllByAlbumId(int albumId)
        {
            var tracks = trackRepository.FindByAlbumId(albumId);
            return Ok(tracks);
        }
    }
}
