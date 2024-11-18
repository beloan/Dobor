using AutoMapper;

namespace Application.Mappings
{
    public class OrganisationMapperProfile : Profile
    {
        public OrganisationMapperProfile()
        {
            CreateMap<Domain.Entities.Organisation, Models.RequestModels.Organisation>().ReverseMap();
            CreateMap<Domain.Entities.Organisation, Models.ResponseModels.Organisation>().ReverseMap();
        }
    }
}
