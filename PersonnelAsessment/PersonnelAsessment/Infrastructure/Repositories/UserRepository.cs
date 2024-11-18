using Application.Abstractions.RepositoryAbstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        DbContexts.ApplicationDbContext _appDbContext;
        public UserRepository(DbContexts.ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> Add(User entity)
        {
            var result = await _appDbContext.Users.AddAsync(entity);
            return result.Entity;
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _appDbContext.Users.Where(u => u.Id!.Equals(id)).ExecuteDeleteAsync();
            return result;
        }

        public async Task<List<User>> GetAll()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(a => a.Email!.Equals(email));
        }

        public async Task<User?> GetById(int id)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id!.Equals(id));
        }

        public async Task<int> UpdateById(int id, User entity)
        {
            return await Task.Run(() => 0);
        }
    }
}
