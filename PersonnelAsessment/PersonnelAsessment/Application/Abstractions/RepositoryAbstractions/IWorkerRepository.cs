using Domain.Entities;

namespace Application.Abstractions.RepositoryAbstractions
{
    public interface IWorkerRepository : IBaseRepository<Worker>
    {
        public Task<Worker?> GetByEmail(string email);
    }
}
