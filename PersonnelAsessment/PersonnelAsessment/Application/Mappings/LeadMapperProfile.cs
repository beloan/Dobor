using AutoMapper;

namespace Application.Mappings
{
    public class LeadMapperProfile : Profile
    {
        public LeadMapperProfile()
        {
            CreateMap<Domain.Entities.Lead, Models.RequestModels.Lead>().ReverseMap();
            CreateMap<Domain.Entities.Lead, Models.ResponseModels.Lead>().ReverseMap();
        }
    }
}
