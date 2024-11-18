using Domain.Entities;

namespace Application.Abstractions.RepositoryAbstractions
{
    public interface IOrganizationRepository : IBaseRepository<Organisation>
    {
        public Task<Organisation?> GetByEmail(string email);
    }
}
