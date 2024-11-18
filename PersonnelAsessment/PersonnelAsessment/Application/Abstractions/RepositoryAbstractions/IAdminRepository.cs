using Domain.Entities;

namespace Application.Abstractions.RepositoryAbstractions
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        public Task<Admin?> GetByEmail(string email);
    }
}
