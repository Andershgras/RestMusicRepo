using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestMusic.Domain.Repositories;
using RestMusic.Domain.Models;

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
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var record = _repo.GetAll().FirstOrDefault(r => r.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        // [Authorize(Roles = "Admin")]
        public ActionResult<MusicRecord> Post([FromBody] MusicRecord newRecord)
        {
            MusicRecord theRecord = _repo.Add(newRecord);
            string uri = Url.RouteUrl(RouteData.Values) + "/" + theRecord.Id;
            return Created(uri, theRecord);
        }
        // DELETE api/<CatsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Admin")]
        public ActionResult<MusicRecord> Delete(int id)
        {
            MusicRecord? removedRecord = _repo.Delete(id);
            if (removedRecord == null) 
            { return NotFound("No such record, id: " + id); }
            return Ok(removedRecord);
        }
    }
}