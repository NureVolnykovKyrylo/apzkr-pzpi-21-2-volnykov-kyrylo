using AquaTrack.Services.Interfaces;
using AquaTrack.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AquaTrack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AquariumController : ControllerBase
    {
        private readonly IAquariumService _aquariumService;

        public AquariumController(IAquariumService aquariumService)
        {
            _aquariumService = aquariumService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AquariumViewModel>> GetAquariumById(int id)
        {
            var aquarium = await _aquariumService.GetAquariumById(id);
            if (aquarium == null)
            {
                return NotFound();
            }

            return aquarium;
        }

        [HttpGet("current-user")]
        public async Task<ActionResult<List<AquariumViewModel>>> GetAquariumsForCurrentUser()
        {
            var aquariums = await _aquariumService.GetAquariumsForCurrentUser();
            if (aquariums == null)
            {
                return NotFound();
            }

            return aquariums;
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<List<AquariumViewModel>>> GetAquariumsForUser(int id)
        {
            var aquariums = await _aquariumService.GetAquariumsForUser(id);
            if (aquariums == null)
            {
                return NotFound();
            }

            return aquariums;
        }

        [HttpPost("user/{id}")]
        public async Task<ActionResult<AquariumViewModel>> AddAquariumForUser(AquariumViewModel aquariumViewModel, int id)
        {
            var addedAquarium = await _aquariumService.AddAquariumForUser(aquariumViewModel, id);
            if (addedAquarium == null)
            {
                return BadRequest();
            }

            return addedAquarium;
        }

        [HttpPut("{id}/user/{userId}")]
        public async Task<ActionResult<AquariumViewModel>> UpdateAquariumForUser(int id, AquariumViewModel aquariumViewModel, int userId)
        {
            if (id != aquariumViewModel.AquariumId)
            {
                return BadRequest();
            }

            var updatedAquarium = await _aquariumService.UpdateAquariumForUser(aquariumViewModel, userId);
            if (updatedAquarium == null)
            {
                return NotFound();
            }

            return updatedAquarium;
        }

        [HttpDelete("{id}/user/{userId}")]
        public async Task<ActionResult> DeleteAquariumForUser(int id, int userId)
        {
            var success = await _aquariumService.DeleteAquariumForUser(id, userId);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("find")]
        public async Task<ActionResult<AquariumViewModel>> FindAquariumByType(string aquariumType)
        {
            var aquarium = await _aquariumService.GetAquariumByType(aquariumType);
            if (aquarium == null)
            {
                return NotFound();
            }

            return aquarium;
        }
    }
}
