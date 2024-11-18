using Application.Abstractions.RepositoryAbstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        DbContexts.ApplicationDbContext _appDbContext;

        public LeadRepository(DbContexts.ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Lead> Add(Lead entity)
        {
            var result = await _appDbContext.Leads.AddAsync(entity);
            return result.Entity;
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _appDbContext.Leads.Where(a => a.Id!.Equals(id)).ExecuteDeleteAsync();
            return result;
        }

        public async Task<List<Lead>> GetAll()
        {
            return await _appDbContext.Leads.ToListAsync();
        }

        public async Task<Lead?> GetByEmail(string email)
        {
            return await _appDbContext.Leads.FirstOrDefaultAsync(a => a.Email!.Equals(email));
        }

        public async Task<Lead?> GetById(int id)
        {
            return await _appDbContext.Leads.FirstOrDefaultAsync(a => a.Id!.Equals(id));
        }

        public async Task<int> UpdateById(int id, Lead entity)
        {
            var tch = await _appDbContext.Leads.FirstOrDefaultAsync(x => x.Id == id);

            if (tch is null) return 0;

            tch.FIO = entity.FIO;
            tch.Education = entity.Education;

            return 1;
        }
    }
}
