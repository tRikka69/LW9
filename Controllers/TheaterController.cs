using Microsoft.AspNetCore.Mvc;
using AmateurTheaterMongo.Models;
using AmateurTheaterMongo.Services;

namespace AmateurTheaterMongo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TheaterController : ControllerBase
    {
        private readonly ITheaterService _service;

        public TheaterController(ITheaterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var theater = await _service.GetByIdAsync(id);
            return theater == null ? NotFound() : Ok(theater);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Theater theater)
        {
            await _service.CreateAsync(theater);
            return CreatedAtAction(nameof(GetById), new { id = theater.Id }, theater);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Theater updatedTheater)
        {
            var success = await _service.UpdateAsync(id, updatedTheater);
            if (!success) return NotFound();
            return Ok(updatedTheater);
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