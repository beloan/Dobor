using Application.Abstractions.RepositoryAbstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        DbContexts.ApplicationDbContext _appDbContext;
        public AdminRepository(DbContexts.ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Admin> Add(Admin entity)
        {
            var result = await _appDbContext.Admins.AddAsync(entity);
            return result.Entity;
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _appDbContext.Admins.Where(a => a.Id!.Equals(id)).ExecuteDeleteAsync();
            return result;
        }

        public async Task<List<Admin>> GetAll()
        {
            return await _appDbContext.Admins.ToListAsync();
        }

        public async Task<Admin?> GetByEmail(string email)
        {
            return await _appDbContext.Admins.FirstOrDefaultAsync(a => a.Email!.Equals(email));
        }

        public async Task<Admin?> GetById(int id)
        {
            return await _appDbContext.Admins.FirstOrDefaultAsync(a => a.Id!.Equals(id));
        }

        public async Task<int> UpdateById(int id, Admin entity)
        {
            return await Task.Run(() => 0);
        }
    }
}
