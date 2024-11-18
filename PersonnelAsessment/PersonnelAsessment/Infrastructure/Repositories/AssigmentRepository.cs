using Application.Abstractions.RepositoryAbstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AssigmentRepository : IAssigmentRepository
    {
        DbContexts.ApplicationDbContext _appDbContext;
        public AssigmentRepository(DbContexts.ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Assigment> Add(Assigment entity)
        {
            var result = await _appDbContext.Assigments.AddAsync(entity);
            return result.Entity;
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _appDbContext.Forms.Where(a => a.Id!.Equals(id)).ExecuteDeleteAsync();
            return result;
        }

        public async Task<List<Assigment>> GetAll()
        {
            return await _appDbContext.Assigments.Include(x => x.Grades).ToListAsync();
        }

        public async Task<Assigment?> GetById(int id)
        {
            return await _appDbContext.Assigments.Include(x => x.Grades).FirstOrDefaultAsync(a => a.Id!.Equals(id));
        }

        public async Task<int> UpdateById(int id, Assigment entity)
        {
            var assigment = await _appDbContext.Assigments.FirstOrDefaultAsync(x => x.Id == id);

            if (assigment is null) return 0;

            assigment.Topic = entity.Topic;

            return 1;
        }
    }
}
