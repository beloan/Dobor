using Application.Abstractions.RepositoryAbstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        DbContexts.ApplicationDbContext _appDbContext;

        public WorkerRepository(DbContexts.ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Worker> Add(Worker entity)
        {
            var result = await _appDbContext.Workers.AddAsync(entity);
            return result.Entity;
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _appDbContext.Workers.Where(a => a.Id!.Equals(id)).ExecuteDeleteAsync();
            return result;
        }

        public async Task<List<Worker>> GetAll()
        {
            return await _appDbContext.Workers.Include(x => x.Form).Include(x => x.Form.TeamLead).Include(x => x.Form.Organisation).ToListAsync();
        }

        public async Task<Worker?> GetByEmail(string email)
        {
            return await _appDbContext.Workers.Include(x => x.Form).Include(x => x.Form.TeamLead).Include(x => x.Form.Organisation).FirstOrDefaultAsync(a => a.Email!.Equals(email));
        }

        public async Task<Worker?> GetById(int id)
        {
            return await _appDbContext.Workers.Include(x => x.Form).Include(x => x.Form.TeamLead).Include(x => x.Form.Organisation).FirstOrDefaultAsync(a => a.Id!.Equals(id));
        }

        public async Task<int> UpdateById(int id, Worker entity)
        {
            var student = await _appDbContext.Workers.FirstOrDefaultAsync(x => x.Id == id);

            if (student is null) return 0;

            student.FIO = entity.FIO;

            return 1;
        }
    }
}
