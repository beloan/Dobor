using AutoMapper;

namespace Application.Mappings
{
    public class GradeMapperProfile : Profile
    {
        public GradeMapperProfile()
        {
            CreateMap<Domain.Entities.Grade, Models.RequestModels.Grade>().ReverseMap();
            CreateMap<Domain.Entities.Grade, Models.ResponseModels.Grade>().ReverseMap();
        }
    }
}
