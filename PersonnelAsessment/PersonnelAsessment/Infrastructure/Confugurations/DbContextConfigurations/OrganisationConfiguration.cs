using Application.Abstractions.ServiceAbstractions;
using Domain.Entities;
using Domain.EntityProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Confugurations.DbContextConfigurations
{
    public class OrganisationConfiguration : IEntityTypeConfiguration<Organisation>
    {
        IHasherService _hasher;
        public OrganisationConfiguration(IHasherService hasher)
        {
            _hasher = hasher;
        }
        public void Configure(EntityTypeBuilder<Organisation> builder)
        {
            builder.ToTable("Organisations");
            builder.HasMany(o => o.Forms).WithOne(f => f.Organisation).HasForeignKey(f => f.OrganisationId);
            builder.HasMany(o => o.Leads).WithMany(t => t.Organisations).UsingEntity(e => e.ToTable("Workers"));
            var salt = _hasher.GetSalt();
            builder.HasData(new Organisation { Id=1, Email = "Andrej.04@mail.ru", Role=Roles.Organisation, Password = _hasher.GetHash("1234qweR", salt), Salt = salt, IsActivated = true });
        }
    }
}
