using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        IAdminRepository _adminRepository;
        IMapper _mapper;
        IHasherService _hasher;
        IUnitOfWork _unitOfWork;

        public AdminService(IAdminRepository adminRepository, IMapper mapper, IHasherService hasher, IUnitOfWork unitOfWork)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
            _hasher = hasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<Application.Models.ResponseModels.Admin> LoginAdmin(Application.Models.RequestModels.Admin admin)
        {
            var realAdmin = await _adminRepository.GetByEmail(admin.Email!);
            if (realAdmin is null)
            {
                throw new Exception("Admin does not exist");
            }

            var hash = _hasher.GetHash(admin.Password, realAdmin.Salt);

            if (hash.Equals(realAdmin.Password))
            {
                return _mapper.Map<Application.Models.ResponseModels.Admin>(realAdmin);
            }
            else
            {
                throw new Exception("Password is not correct");
            }
        }

        public async Task<Application.Models.ResponseModels.Admin> RegisterAdmin(Application.Models.RequestModels.Admin admin)
        {
            var realAdmin = await _adminRepository.GetByEmail(admin.Email!);
            if (realAdmin is not null)
            {
                throw new Exception("Admin is already exists");
            }

            var adminR = _mapper.Map<Admin>(admin);
            adminR.Salt = _hasher.GetSalt();
            adminR.Password = _hasher.GetHash(adminR.Password, adminR.Salt);

            var result = await _adminRepository.Add(adminR);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Application.Models.ResponseModels.Admin>(result);
        }
    }
}
