using Application.Abstractions.RepositoryAbstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserImageRepository : IUserImageRepository
    {
        DbContexts.ApplicationDbContext _appDbContext;

        public UserImageRepository(DbContexts.ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<UserImage> Add(UserImage entity)
        {
            var result = await _appDbContext.UserImages.AddAsync(entity);
            return result.Entity;
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _appDbContext.UserImages.Where(a => a.Id!.Equals(id)).ExecuteDeleteAsync();
            return result;
        }

        public async Task<List<UserImage>> GetAll()
        {
            return await _appDbContext.UserImages.ToListAsync();
        }

        public async Task<UserImage?> GetById(int id)
        {
            return await _appDbContext.UserImages.FirstOrDefaultAsync(a => a.Id!.Equals(id));
        }

        public async Task<UserImage?> GetImageByUser(int id)
        {
            return await _appDbContext.UserImages.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<int> UpdateById(int id, UserImage entity)
        {
            var image = await _appDbContext.UserImages.FirstOrDefaultAsync(x => x.Id == id);

            if (image is null) return 0;

            image.ImageName = entity.ImageName;

            return 1;
        }
    }
}
