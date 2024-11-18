using AutoMapper;

namespace Application.Mappings
{
    public class FormMapperProfile : Profile
    {
        public FormMapperProfile()
        {
            CreateMap<Domain.Entities.Form, Models.RequestModels.Form>().ReverseMap();
            CreateMap<Domain.Entities.Form, Models.ResponseModels.Form>().ReverseMap();
        }
    }
}
