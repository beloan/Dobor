using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Confugurations.DbContextConfigurations
{
    public class JobListConfiguration : IEntityTypeConfiguration<JobList>
    {
        public void Configure(EntityTypeBuilder<JobList> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).UseSerialColumn();
            builder.HasOne(s => s.Form).WithMany(f => f.JobLists).HasForeignKey(s => s.FormId);
            builder.HasData(new JobList
            {
                Id = 1,
                FormId = 1,
                Day = "Monday",
                IndexNum = 1
            });
            builder.Property(s => s.IndexNum);
		}
	}
}
