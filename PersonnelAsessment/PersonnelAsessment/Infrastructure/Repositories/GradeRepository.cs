using Application.Abstractions.RepositoryAbstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        DbContexts.ApplicationDbContext _appDbContext;

        public GradeRepository(DbContexts.ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Grade> Add(Grade entity)
        {
            var result = await _appDbContext.Grades.AddAsync(entity);
            return result.Entity;
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _appDbContext.Grades.Where(a => a.Id!.Equals(id)).ExecuteDeleteAsync();
            return result;
        }

        public async Task<List<Grade>> GetAll()
        {
            return await _appDbContext.Grades.ToListAsync();
        }

        public async Task<Grade?> GetById(int id)
        {
            return await _appDbContext.Grades.FirstOrDefaultAsync(a => a.Id!.Equals(id));
        }

        public async Task<int> UpdateById(int id, Grade entity)
        {
            var grade = await _appDbContext.Grades.FirstOrDefaultAsync(x => x.Id == id);

            if (grade is null) return 0;

            grade.Value = entity.Value;

            return 1;
        }
    }
}
