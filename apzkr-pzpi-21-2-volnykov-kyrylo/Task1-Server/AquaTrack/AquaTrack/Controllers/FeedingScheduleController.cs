using AquaTrack.Services.Interfaces;
using AquaTrack.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AquaTrack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedingScheduleController : ControllerBase
    {
        private readonly IFeedingScheduleService _feedingScheduleService;

        public FeedingScheduleController(IFeedingScheduleService feedingScheduleService)
        {
            _feedingScheduleService = feedingScheduleService;
        }

        // GET: api/FeedingSchedule
        [HttpGet]
        public async Task<IActionResult> GetFeedingSchedulesForCurrentUser()
        {
            var feedingSchedules = await _feedingScheduleService.GetFeedingSchedulesForCurrentUser();
            if (feedingSchedules == null)
            {
                return NotFound();
            }
            return Ok(feedingSchedules);
        }

        // GET: api/FeedingSchedule/aquarium/5
        [HttpGet("aquarium/{id}")]
        public async Task<IActionResult> GetFeedingSchedulesByAquariumId(int id)
        {
            var feedingSchedules = await _feedingScheduleService.GetFeedingSchedulesByAquariumId(id);
            if (feedingSchedules == null)
            {
                return NotFound();
            }
            return Ok(feedingSchedules);
        }

        // GET: api/FeedingSchedule/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedingScheduleById(int id)
        {
            var feedingSchedule = await _feedingScheduleService.GetFeedingScheduleById(id);
            if (feedingSchedule == null)
            {
                return NotFound();
            }
            return Ok(feedingSchedule);
        }

        // POST: api/FeedingSchedule
        [HttpPost]
        public async Task<IActionResult> AddFeedingScheduleForCurrentUser([FromBody] FeedingScheduleViewModel feedingScheduleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedFeedingScheduleViewModel = await _feedingScheduleService.AddFeedingScheduleForCurrentUser(feedingScheduleViewModel);

            if (addedFeedingScheduleViewModel == null)
            {
                return Unauthorized(); // Or any other status code depending on reason
            }

            return Ok(addedFeedingScheduleViewModel);
        }

        // PUT: api/FeedingSchedule/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedingScheduleForCurrentUser(int id, [FromBody] FeedingScheduleViewModel feedingScheduleViewModel)
        {
            if (id != feedingScheduleViewModel.FeedingScheduleId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedFeedingScheduleViewModel = await _feedingScheduleService.UpdateFeedingScheduleForCurrentUser(feedingScheduleViewModel);

            if (updatedFeedingScheduleViewModel == null)
            {
                return Unauthorized(); // Or any other status code depending on reason
            }

            return Ok(updatedFeedingScheduleViewModel);
        }

        // DELETE: api/FeedingSchedule/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedingScheduleForCurrentUser(int id)
        {
            var success = await _feedingScheduleService.DeleteFeedingScheduleForCurrentUser(id);

            if (!success)
            {
                return Unauthorized(); // Or any other status code depending on reason
            }

            return NoContent();
        }
    }
}
