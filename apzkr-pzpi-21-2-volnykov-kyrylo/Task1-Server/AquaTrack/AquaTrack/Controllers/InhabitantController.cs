using AquaTrack.Models;
using AquaTrack.Services.Interfaces;
using AquaTrack.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AquaTrack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InhabitantController : ControllerBase
    {
        private readonly IInhabitantService _inhabitantService;

        public InhabitantController(IInhabitantService inhabitantService)
        {
            _inhabitantService = inhabitantService;
        }

        // GET: api/Inhabitant/aquarium/5
        [HttpGet("aquarium/{id}")]
        public async Task<IActionResult> GetInhabitantsForCurrentUser(int id)
        {
            var inhabitants = await _inhabitantService.GetInhabitantsByAquariumId(id);
            if (inhabitants == null)
            {
                return NotFound();
            }
            return Ok(inhabitants);
        }

        // GET: api/Inhabitant/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInhabitantById(int id)
        {
            var inhabitant = await _inhabitantService.GetInhabitantById(id);
            if (inhabitant == null)
            {
                return NotFound();
            }
            return Ok(inhabitant);
        }

        // POST: api/Inhabitant
        [HttpPost]
        public async Task<IActionResult> AddInhabitantForCurrentUser([FromBody] InhabitantViewModel inhabitantViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedInhabitantViewModel = await _inhabitantService.AddInhabitantForCurrentUser(inhabitantViewModel);

            if (addedInhabitantViewModel == null)
            {
                return Unauthorized(); // Or any other status code depending on reason
            }

            return Ok(addedInhabitantViewModel);
        }

        // PUT: api/Inhabitant/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInhabitantForCurrentUser(int id, [FromBody] InhabitantViewModel inhabitantViewModel)
        {
            if (id != inhabitantViewModel.InhabitantId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedInhabitantViewModel = await _inhabitantService.UpdateInhabitantForCurrentUser(inhabitantViewModel);

            if (updatedInhabitantViewModel == null)
            {
                return Unauthorized(); // Or any other status code depending on reason
            }

            return Ok(updatedInhabitantViewModel);
        }

        // DELETE: api/Inhabitant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInhabitantForCurrentUser(int id)
        {
            var success = await _inhabitantService.DeleteInhabitantForCurrentUser(id);

            if (!success)
            {
                return Unauthorized(); // Or any other status code depending on reason
            }

            return NoContent();
        }
    }
}
