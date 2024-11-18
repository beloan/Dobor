using Infrastructure.Confugurations.DbContextConfigurations;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class DbContextConfigurations
    {
        public static ModelBuilder ApplyConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new OrganisationConfiguration(new HasherService()));
            modelBuilder.ApplyConfiguration(new LeadConfiguration(new HasherService()));
            modelBuilder.ApplyConfiguration(new WorkerConfiguration(new HasherService()));
            modelBuilder.ApplyConfiguration(new AssigmentConfiguration());
            modelBuilder.ApplyConfiguration(new GradeConfiguration());
            modelBuilder.ApplyConfiguration(new FormConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration(new HasherService()));
            modelBuilder.ApplyConfiguration(new JobListConfiguration());
            modelBuilder.ApplyConfiguration(new UserImageConfiguration());

            return modelBuilder;
        }
    }
}
