using Microsoft.AspNetCore.Mvc;
using AmateurTheaterMongo.Models;
using AmateurTheaterMongo.Services;
using FluentValidation;

namespace AmateurTheaterMongo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayController : ControllerBase
    {
        private readonly IPlayService _service;
        private readonly IValidator<Play> _validator;

        public PlayController(IPlayService service, IValidator<Play> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var play = await _service.GetByIdAsync(id);
            return play == null ? NotFound() : Ok(play);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Play play)
        {
            var result = await _validator.ValidateAsync(play);
            if (!result.IsValid) return BadRequest(result.Errors);

            await _service.CreateAsync(play);
            return CreatedAtAction(nameof(GetById), new { id = play.Id }, play);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Play updatedPlay)
        {
            var success = await _service.UpdateAsync(id, updatedPlay);
            if (!success) return NotFound();
            return Ok(updatedPlay);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}