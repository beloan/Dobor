using AutoMapper;

namespace Application.Mappings
{
    public class WorkerMapperProfile : Profile
    {
        public WorkerMapperProfile()
        {
            CreateMap<Domain.Entities.Worker, Models.RequestModels.Worker>().ReverseMap();
            CreateMap<Domain.Entities.Worker, Models.ResponseModels.Worker>().ReverseMap();
        }
    }
}
