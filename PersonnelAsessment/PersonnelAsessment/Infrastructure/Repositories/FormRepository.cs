using Application.Abstractions.RepositoryAbstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FormRepository : IFormRepository
    {
        DbContexts.ApplicationDbContext _appDbContext;

        public FormRepository(DbContexts.ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Form> Add(Form entity)
        {
            var result = await _appDbContext.Forms.AddAsync(entity);
            return result.Entity;
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _appDbContext.Forms.Where(a => a.Id!.Equals(id)).ExecuteDeleteAsync();
            return result;
        }

        public async Task<List<Form>> GetAll()
        {
            return await _appDbContext.Forms.Include(x => x.Organisation).ToListAsync();
        }

        public async Task<Form?> GetById(int id)
        {
            return await _appDbContext.Forms.Include(x => x.Organisation).FirstOrDefaultAsync(a => a.Id!.Equals(id));
        }

        public async Task<int> UpdateById(int id, Form entity)
        {
            var form = await _appDbContext.Forms.FirstOrDefaultAsync(x => x.Id == id);
            if (form is null) return 0;

            form.LeadId = entity.LeadId;
            form.Number = entity.Number;

            return 1;
        }
    }
}
