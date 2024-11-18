using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Confugurations.DbContextConfigurations
{
    public class UserImageConfiguration : IEntityTypeConfiguration<UserImage>
    {

        public void Configure(EntityTypeBuilder<UserImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseSerialColumn();
            builder.HasOne(x => x.User).WithOne(u => u.Image).HasForeignKey<UserImage>(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.UserId).IsRequired(false);
        }
    }
}
