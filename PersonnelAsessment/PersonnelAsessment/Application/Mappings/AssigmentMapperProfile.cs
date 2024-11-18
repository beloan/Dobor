using AutoMapper;

namespace Application.Mappings
{
    public class AssigmentMapperProfile : Profile
    {
        public AssigmentMapperProfile()
        {
            CreateMap<Domain.Entities.Assigment, Models.RequestModels.Assigment>().ReverseMap();
            CreateMap<Domain.Entities.Assigment, Models.ResponseModels.Assigment>().ReverseMap();
        }
    }
}
