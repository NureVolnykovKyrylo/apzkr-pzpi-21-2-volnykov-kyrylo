using AquaTrack.Models;
using AquaTrack.Repositories.Interfaces;
using AquaTrack.Services.Interfaces;
using AquaTrack.ViewModels;
using AutoMapper;

namespace AquaTrack.Services
{
    public class ResearchReportService : IResearchReportService
    {
        private readonly IResearchReportRepository _researchReportRepository;
        private readonly IMapper _mapper;

        public ResearchReportService(IResearchReportRepository researchReportRepository, IMapper mapper)
        {
            _researchReportRepository = researchReportRepository;
            _mapper = mapper;
        }

        public async Task<List<ResearchReportViewModel>> GetAllResearchReports()
        {
            var reports = await _researchReportRepository.GetAllResearchReportsAsync();
            var reportViewModels = _mapper.Map<List<ResearchReportViewModel>>(reports);
            return reportViewModels;
        }

        public async Task<ResearchReportViewModel> GetResearchReportById(int researchReportId)
        {
            var report = await _researchReportRepository.GetResearchReportByIdAsync(researchReportId);
            var reportViewModel = _mapper.Map<ResearchReportViewModel>(report);
            return reportViewModel;
        }

        public async Task<ResearchReportViewModel> AddResearchReport(ResearchReportViewModel reportViewModel)
        {
            var report = _mapper.Map<ResearchReport>(reportViewModel);
            var addedReport = await _researchReportRepository.AddResearchReportAsync(report);
            var addedReportViewModel = _mapper.Map<ResearchReportViewModel>(addedReport);
            return addedReportViewModel;
        }

        public async Task<ResearchReportViewModel> UpdateResearchReport(ResearchReportViewModel reportViewModel)
        {
            var report = _mapper.Map<ResearchReport>(reportViewModel);
            var updatedReport = await _researchReportRepository.UpdateResearchReportAsync(report);
            var updatedReportViewModel = _mapper.Map<ResearchReportViewModel>(updatedReport);
            return updatedReportViewModel;
        }

        public async Task DeleteResearchReport(int researchReportId)
        {
            await _researchReportRepository.DeleteResearchReportAsync(researchReportId);
        }
    }
}
