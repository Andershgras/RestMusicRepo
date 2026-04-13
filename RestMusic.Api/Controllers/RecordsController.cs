using Microsoft.AspNetCore.Mvc;
using RestMusic.Domain.Repositories;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly MusicRepoList _repo;
        //Constructor
        public RecordsController(MusicRepoList repo)
        {
            _repo = repo;
        }
        //GET methods
        [HttpGet]
        public ActionResult Get([FromQuery] string? title, [FromQuery] string? artist)
        {
            var results = _repo.GetByTitleOgArtist(title, artist);
            return Ok(results);
        }
    }
}
