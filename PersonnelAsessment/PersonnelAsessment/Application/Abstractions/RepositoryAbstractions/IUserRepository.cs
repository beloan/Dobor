using Domain.Entities;

namespace Application.Abstractions.RepositoryAbstractions
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User?> GetByEmail(string email);
    }
}
