using Application.Abstractions.RepositoryAbstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class JobListRepository : IJobListRepository
    {
        DbContexts.ApplicationDbContext _appDbContext;

        public JobListRepository(DbContexts.ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<JobList> Add(JobList entity)
        {
            var result = await _appDbContext.JobLists.AddAsync(entity);
            return result.Entity;
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _appDbContext.JobLists.Where(a => a.Id!.Equals(id)).ExecuteDeleteAsync();
            return result;
        }

        public async Task<List<JobList>> GetAll()
        {
            return await _appDbContext.JobLists
                .Include(x => x.Form)
                .ToListAsync();
        }

        public async Task<JobList?> GetById(int id)
        {
            return await _appDbContext.JobLists
                .Include(x => x.Form)
                .Include(x => x.Form!.Organisation)
                .FirstOrDefaultAsync(a => a.Id!.Equals(id));
        }

        public async Task<int> UpdateById(int id, JobList entity)
        {
            var jobList = await _appDbContext.JobLists.FirstOrDefaultAsync(x => x.Id == id);

            if (jobList is null) return 0;

            return 1;
        }
    }
}
