using Application.Abstractions.RepositoryAbstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrganisationRepository : IOrganizationRepository
    {
        DbContexts.ApplicationDbContext _appDbContext;
        public OrganisationRepository(DbContexts.ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Organisation> Add(Organisation entity)
        {
            var result = await _appDbContext.Organisations.AddAsync(entity);
            return result.Entity;
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _appDbContext.Organisations.Where(u => u.Id!.Equals(id)).ExecuteDeleteAsync();
            return result;
        }

        public async Task<List<Organisation>> GetAll()
        {
            return await _appDbContext.Organisations.Include(x => x.Leads).ToListAsync();
        }

        public async Task<Organisation?> GetByEmail(string email)
        {
            return await _appDbContext.Organisations.Include(o => o.Leads).FirstOrDefaultAsync(a => a.Email!.Equals(email));
        }

        public async Task<Organisation?> GetById(int id)
        {
            return await _appDbContext.Organisations.Include(o => o.Leads).FirstOrDefaultAsync(u => u.Id!.Equals(id));
        }

        public async Task<int> UpdateById(int id, Organisation entity)
        {
            var org = await _appDbContext.Organisations.FirstOrDefaultAsync(x => x.Id == id);

            if (org is null) return 0;

            org.INN = entity.INN;
            org.Address = entity.Address;
            org.Name = entity.Name;

            return 1;
        }
    }
}
