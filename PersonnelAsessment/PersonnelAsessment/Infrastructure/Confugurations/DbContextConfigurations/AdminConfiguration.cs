using Application.Abstractions.ServiceAbstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Confugurations.DbContextConfigurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        IHasherService _hasher;
        public AdminConfiguration(IHasherService hasher)
        {
            _hasher = hasher;
        }
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins", "private");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).UseSerialColumn();
            builder.HasIndex(a => a.Email).IsUnique();
            var salt = _hasher.GetSalt();
            builder.HasData(new Admin { Id = 1, Email = "admin1@ymail.com", Password = _hasher.GetHash("1234qweR", salt), Salt = salt});
        }
    }
}
