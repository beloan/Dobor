using AutoMapper;

namespace Application.Mappings
{
    public class AdminMapperProfile : Profile 
    {
        public AdminMapperProfile()
        {
            CreateMap<Domain.Entities.Admin, Models.RequestModels.Admin>().ReverseMap();
            CreateMap<Domain.Entities.Admin, Models.ResponseModels.Admin>().ReverseMap();
        }
    }
}
