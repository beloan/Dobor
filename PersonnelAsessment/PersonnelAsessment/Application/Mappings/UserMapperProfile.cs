using AutoMapper;

namespace Application.Mappings
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<Domain.Entities.User, Models.RequestModels.User>().ReverseMap();
            CreateMap<Domain.Entities.User, Models.ResponseModels.User>().ReverseMap();
        }
    }
}
