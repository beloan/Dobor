namespace Application.Abstractions.RepositoryAbstractions
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<List<T>> GetAll();
        public Task<T?> GetById(int id);
        public Task<T> Add(T entity);
        public Task<int> UpdateById(int id, T entity);
        public Task<int> DeleteById(int id);
    }
}
