using AquaTrack.Services.Interfaces;
using AquaTrack.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AquaTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisReportController : ControllerBase
    {
        private readonly IAnalysisReportService _analysisReportService;

        public AnalysisReportController(IAnalysisReportService analysisReportService)
        {
            _analysisReportService = analysisReportService ?? throw new ArgumentNullException(nameof(analysisReportService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnalysisReports()
        {
            var analysisReports = await _analysisReportService.GetAllAnalysisReports();
            return Ok(analysisReports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnalysisReportById(int id)
        {
            var analysisReport = await _analysisReportService.GetAnalysisReportById(id);
            if (analysisReport == null)
            {
                return NotFound();
            }

            return Ok(analysisReport);
        }

        [HttpPost("{researchReportId}")]
        public async Task<IActionResult> AddAnalysisReport(int researchReportId)
        {
            var addedAnalysisReport = await _analysisReportService.AddAnalysisReport(researchReportId);
            if (addedAnalysisReport == null)
            {
                return BadRequest();
            }

            return Ok(addedAnalysisReport);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnalysisReport(int id, [FromBody] AnalysisReportViewModel analysisReportViewModel)
        {
            if (id != analysisReportViewModel.AnalysisReportId)
            {
                return BadRequest();
            }

            var updatedAnalysisReport = await _analysisReportService.UpdateAnalysisReport(analysisReportViewModel);
            if (updatedAnalysisReport == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnalysisReport(int id)
        {
            await _analysisReportService.DeleteAnalysisReport(id);
            return NoContent();
        }
    }
}
