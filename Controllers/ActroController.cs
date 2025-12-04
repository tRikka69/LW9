using Microsoft.AspNetCore.Mvc;
using AmateurTheaterMongo.Models;
using AmateurTheaterMongo.Services;
using FluentValidation;

namespace AmateurTheaterMongo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _service;
        private readonly IValidator<Actor> _validator;

        public ActorController(IActorService service, IValidator<Actor> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var actor = await _service.GetByIdAsync(id);
            return actor == null ? NotFound() : Ok(actor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Actor actor)
        {
            var result = await _validator.ValidateAsync(actor);
            if (!result.IsValid) return BadRequest(result.Errors);

            await _service.CreateAsync(actor);
            return CreatedAtAction(nameof(GetById), new { id = actor.Id }, actor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Actor updatedActor)
        {
            var success = await _service.UpdateAsync(id, updatedActor);
            if (!success) return NotFound();
            return Ok(updatedActor);
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