using Domain.Entities;
using Domain.EntityProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Confugurations.DbContextConfigurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).UseSerialColumn();
            builder.HasOne(g => g.Assigment).WithMany(l => l.Grades).HasForeignKey(g => g.AssigmentId);
            builder.HasOne(g => g.Worker).WithMany(st => st.Grades).HasForeignKey(g => g.WorkerId);
            builder.HasData(new Grade
            {
                Id = 1,
                AssigmentId = 1,
                WorkerId = 2,
                Value = GradeTypes.Five
            });
        }
    }
}
