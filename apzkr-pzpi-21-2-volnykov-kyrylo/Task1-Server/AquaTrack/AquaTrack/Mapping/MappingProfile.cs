namespace AquaTrack.Mapping
{
    using AutoMapper;
    using AquaTrack.Models;
    using AquaTrack.ViewModels;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AquariumViewModel, Aquarium>().ReverseMap();

            CreateMap<InhabitantViewModel, Inhabitant>().ReverseMap();

            CreateMap<FeedingScheduleViewModel, FeedingSchedule>().ReverseMap();

            CreateMap<SensorDataViewModel, SensorData>().ReverseMap();
            CreateMap<SensorDataUpdateViewModel, SensorData>().ReverseMap();

            CreateMap<ResearchReportViewModel, ResearchReport>().ReverseMap();

            CreateMap<AnalysisReportViewModel, AnalysisReport>().ReverseMap();

            CreateMap<UserLoginViewModel, User>().ReverseMap();
            CreateMap<UserRegisterViewModel, User>().ReverseMap();
            CreateMap<UserRegisterViewModel, UserViewModel>().ReverseMap();
            CreateMap<UserUpdateViewModel, User>().ReverseMap();
            CreateMap<UserUtilityViewModel, User>().ReverseMap();

            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.Aquariums, opt => opt.MapFrom(src => src.Aquariums))
                .ReverseMap();
        }
    }
}
