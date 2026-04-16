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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<MusicRecord> Post([FromBody] MusicRecord newRecord)
        {
            MusicRecord theRecord = _repo.Add(newRecord);
            return CreatedAtAction(nameof(GetById), new { id = theRecord.Id }, theRecord);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MusicRecord> Put(int id, [FromBody] MusicRecord value)
        {
            MusicRecord? updatedRecord = _repo.Update(id, value);

            if (updatedRecord == null)
            {
                return NotFound("No such record, id: " + id);
            }

            return Ok(updatedRecord);
        }
    }
}