using Application.Abstractions.ServiceAbstractions;

namespace Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        DbContexts.ApplicationDbContext _appDbContext;

        public UnitOfWork(DbContexts.ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
