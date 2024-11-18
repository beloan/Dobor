using Application.Abstractions.ServiceAbstractions;
using Domain.Entities;
using Domain.EntityProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Confugurations.DbContextConfigurations
{
    public class FormConfiguration : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).UseSerialColumn();
            builder.HasOne(f => f.Organisation).WithMany(o => o.Forms).HasForeignKey(f => f.OrganisationId);
            builder.HasOne(f => f.TeamLead).WithMany(t => t.Forms).HasForeignKey(f =>f.LeadId);
            builder.HasMany(f => f.JobLists).WithOne(s => s.Form).HasForeignKey(s => s.FormId);
            builder.HasData(new Form
            {
                Id = 1,
                LeadId = 3,
                Number = 1,
                OrganisationId = 1
            });
        }
    }
}
