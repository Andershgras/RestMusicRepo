using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestMusic.Domain.Repositories;

namespace RestMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly MusicRepoList _repo;

        public RecordsController(MusicRepoList repo)
        {
            _repo = repo;
        }
        //GET records
        [HttpGet]
        // [Authorize]
        [AllowAnonymous]
        public ActionResult GetAll([FromQuery] string? title, [FromQuery] string? artist)
        {
            var records = _repo.GetAll();

            if (!string.IsNullOrWhiteSpace(title))
            {
                records = records.Where(r => r.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(artist))
            {
                records = records.Where(r => r.Artist.Contains(artist, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(records);
        }
    }
}