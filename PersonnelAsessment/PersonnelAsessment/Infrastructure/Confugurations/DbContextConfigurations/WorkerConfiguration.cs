using Application.Abstractions.ServiceAbstractions;
using Domain.Entities;
using Domain.EntityProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Confugurations.DbContextConfigurations
{
    public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
    {
        IHasherService _hasher;

        public WorkerConfiguration(IHasherService hasher)
        {
            _hasher = hasher;
        }

        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.ToTable("Workers");
            var salt = _hasher.GetSalt();
            builder.HasData(new Worker
            {
                Id = 2,
                Email = "andrej@example.ru",
                Role = Roles.Worker,
                Password = _hasher.GetHash("1234qweR", salt),
                Salt = salt,
                IsActivated = true,
                FIO = "Зубенко IVan Петрович",
                FormId = 1
            });
        }
    }
}
