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
        private readonly IMusicRepoList _repo;

        public RecordsController(IMusicRepoList repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAll([FromQuery] string? title, [FromQuery] string? artist)
        {
            var records = _repo.GetByTitleOgArtist(title, artist);
            return Ok(records);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult GetById(int id)
        {
            var record = _repo.GetById(id);

            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<MusicRecord> Post([FromBody] MusicRecord newRecord)
        {
            newRecord.Title = newRecord.Title?.Trim() ?? string.Empty;
            newRecord.Artist = newRecord.Artist?.Trim() ?? string.Empty;

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            MusicRecord theRecord = _repo.Add(newRecord);
            return CreatedAtAction(nameof(GetById), new { id = theRecord.Id }, theRecord);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MusicRecord> Delete(int id)
        {
            MusicRecord? removedRecord = _repo.Delete(id);

            if (removedRecord == null)
            {
                return NotFound("No such record, id: " + id);
            }

            return Ok(removedRecord);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MusicRecord> Put(int id, [FromBody] MusicRecord value)
        {
            value.Title = value.Title?.Trim() ?? string.Empty;
            value.Artist = value.Artist?.Trim() ?? string.Empty;

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            MusicRecord? updatedRecord = _repo.Update(id, value);

            if (updatedRecord == null)
            {
                return NotFound("No such record, id: " + id);
            }

            return Ok(updatedRecord);
        }
    }
}