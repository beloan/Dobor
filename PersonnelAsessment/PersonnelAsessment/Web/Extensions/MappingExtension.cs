using Application.Mappings;

namespace Web.Extensions
{
    public static class MappingExtension
    {
        public static void AddProfiles(this AutoMapper.IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<AdminMapperProfile>();
            cfg.AddProfile<FormMapperProfile>();
            cfg.AddProfile<GradeMapperProfile>();
            cfg.AddProfile<AssigmentMapperProfile>();
            cfg.AddProfile<OrganisationMapperProfile>();
            cfg.AddProfile<JobListMapperProfile>();
            cfg.AddProfile<WorkerMapperProfile>();
            cfg.AddProfile<LeadMapperProfile>();
            cfg.AddProfile<UserMapperProfile>();
        }
    }
}
