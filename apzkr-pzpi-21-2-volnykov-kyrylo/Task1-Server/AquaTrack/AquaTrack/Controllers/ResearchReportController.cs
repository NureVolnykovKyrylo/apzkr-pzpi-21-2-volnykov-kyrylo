using AquaTrack.Services.Interfaces;
using AquaTrack.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AquaTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResearchReportController : ControllerBase
    {
        private readonly IResearchReportService _reportService;

        public ResearchReportController(IResearchReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResearchReportViewModel>>> GetAllResearchReports()
        {
            var reports = await _reportService.GetAllResearchReports();
            if (reports == null || reports.Count == 0)
            {
                return NotFound();
            }
            return reports;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResearchReportViewModel>> GetResearchReportById(int id)
        {
            var report = await _reportService.GetResearchReportById(id);
            if (report == null)
            {
                return NotFound();
            }
            return report;
        }

        [HttpPost]
        public async Task<ActionResult<ResearchReportViewModel>> AddResearchReport(ResearchReportViewModel report)
        {
            var addedReport = await _reportService.AddResearchReport(report);
            if (addedReport == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetResearchReportById), new { id = addedReport.ResearchReportId }, addedReport);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResearchReportViewModel>> UpdateResearchReport(int id, ResearchReportViewModel report)
        {
            if (id != report.ResearchReportId)
            {
                return BadRequest();
            }

            var updatedReport = await _reportService.UpdateResearchReport(report);
            if (updatedReport == null)
            {
                return NotFound();
            }
            return updatedReport;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResearchReport(int id)
        {
            await _reportService.DeleteResearchReport(id);
            return NoContent();
        }
    }
}
