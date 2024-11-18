using Application.Abstractions.ServiceAbstractions;
using Domain.Entities;
using Domain.EntityProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Confugurations.DbContextConfigurations
{
    public class LeadConfiguration : IEntityTypeConfiguration<Lead>
    {
        IHasherService _hasher;

        public LeadConfiguration(IHasherService hasher)
        {
            _hasher = hasher;
        }

        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.ToTable("Leads");
            builder.HasMany(t => t.Forms).WithOne(f => f.TeamLead).HasForeignKey(f => f.LeadId);
            var salt = _hasher.GetSalt();
            builder.HasData(new Lead
            {
                Id = 3,
                Email = "andrej_cool@example.ru",
                Role = Roles.Lead,
                Password = _hasher.GetHash("1234qweR", salt),
                Salt = salt,
                IsActivated = true,
                FIO = "Александр Македонский"
            });
        }
    }
}
