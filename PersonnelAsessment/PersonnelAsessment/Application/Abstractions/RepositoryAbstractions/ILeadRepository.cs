using Domain.Entities;

namespace Application.Abstractions.RepositoryAbstractions
{
    public interface ILeadRepository : IBaseRepository<Lead>
    {
        public Task<Lead?> GetByEmail(string email);
    }
}
