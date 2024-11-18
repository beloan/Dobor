using AutoMapper;

namespace Application.Mappings
{
    public class JobListMapperProfile : Profile
    {
        public JobListMapperProfile()
        {
            CreateMap<Domain.Entities.JobList, Models.RequestModels.JobList>().ReverseMap();
            CreateMap<Domain.Entities.JobList, Models.ResponseModels.JobList>().ReverseMap();
        }
    }
}
