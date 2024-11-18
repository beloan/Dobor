using Domain.Entities;

namespace Application.Abstractions.RepositoryAbstractions
{
    public interface IUserImageRepository : IBaseRepository<UserImage>
    {
        public Task<UserImage?> GetImageByUser(int id);
    }
}
