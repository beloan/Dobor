using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Confugurations.DbContextConfigurations
{
    public class AssigmentConfiguration : IEntityTypeConfiguration<Assigment>
    {
        public void Configure(EntityTypeBuilder<Assigment> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).UseSerialColumn();
            builder.HasOne(l => l.Form).WithMany(f => f.Assigments).HasForeignKey(l => l.FormId);
            builder.HasMany(l => l.Grades).WithOne(g => g.Assigment).HasForeignKey(g => g.AssigmentId);
            builder.HasData(new Assigment
            {
                Id = 1,
                Date = new DateOnly(2024, 4, 10),
                FormId = 1,
                Topic = "Устройство сердца"
            });
            builder.Property(l => l.Date).HasColumnType("date");
        }
    }
}
