using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        IMapper _mapper;
        IHasherService _hasher;
        IUnitOfWork _unitOfWork;
        IEmailService _emailService;
        IDistributedCache _cache;
        ITokenGeneratorService _tokenGeneratorService;
        IUserImageRepository _userImageRepository;

        public UserService(IUserRepository userRepository, IMapper mapper,
            IHasherService hasher, IUnitOfWork unitOfWork,
            IEmailService emailService, IDistributedCache cache,
            ITokenGeneratorService tokenGeneratorService,
            IUserImageRepository userImageRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _hasher = hasher;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _cache = cache;
            _tokenGeneratorService = tokenGeneratorService;
            _userImageRepository = userImageRepository;
        }

        public async Task SendConfirmationMessage(string? email)
        {
            var token = await _tokenGeneratorService.GenerateToken(email!);
            var link = $"<a href=\"http://localhost:5279/user/confirm?token={token}\">Подтвердить</a>";

            _ = _emailService.SendEmailAsync(link, email!, "Подтверждение учётной записи на UDEO");
            await AddTokenToCache(token, email!);
        }

        public async Task<Application.Models.ResponseModels.User> ActivateUser(string? email)
        {
            var user = await _userRepository.GetByEmail(email!);
            if (user is null) throw new Exception("User does not exist");

            user.IsActivated = true;
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Application.Models.ResponseModels.User>(user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            var result = await _userRepository.DeleteById(id);
            await _unitOfWork.SaveChangesAsync();

            return result == 1;
        }

        public async Task<Application.Models.ResponseModels.User?> GetUser(string? email)
        {
            var result = await _userRepository.GetByEmail(email!);

            return _mapper.Map<Application.Models.ResponseModels.User>(result);
        }

        public async Task<Application.Models.ResponseModels.User> LoginUser(Application.Models.RequestModels.User user)
        {
            var realUser = await _userRepository.GetByEmail(user.Email!);
            if (realUser is null)
            {
                throw new Exception("User does not exist");
            }

            if (!realUser.IsActivated) throw new NotImplementedException();

            var hash = _hasher.GetHash(user.Password, realUser.Salt);

            if (hash.Equals(realUser.Password))
            {
                return _mapper.Map<Application.Models.ResponseModels.User>(realUser);
            }
            else
            {
                throw new Exception("Password is not correct");
            }
        }

        public async Task<Application.Models.ResponseModels.User> RegisterUser(Application.Models.RequestModels.User user)
        {
            var realUser = await _userRepository.GetByEmail(user.Email!);
            if(realUser is not null)
            {
                throw new Exception("User is already exist");
            }

            var userR = _mapper.Map<User>(user);
            userR.Salt = _hasher.GetSalt();
            userR.Password = _hasher.GetHash(userR.Password, userR.Salt);
            userR.IsActivated = false;
            
            var result = await _userRepository.Add(userR);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Application.Models.ResponseModels.User>(result);
        }

        public Task<Application.Models.ResponseModels.User?> UpdateUser(int id, Application.Models.RequestModels.User user)
        {
            throw new NotImplementedException();
        }

        public async Task AddTokenToCache(string token, string email)
        {
            var opt = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTimeOffset.UtcNow.AddDays(7));
            await _cache.SetStringAsync(token, email, opt);
        }

        public async Task<Application.Models.ResponseModels.User?> GetUserByEmail(string? email)
        {
            return _mapper.Map<Application.Models.ResponseModels.User>(await _userRepository.GetByEmail(email!));
        }

        public async Task<Application.Models.ResponseModels.User?> GetUserById(int id)
        {
            return _mapper.Map<Application.Models.ResponseModels.User>(await _userRepository.GetById(id));
        }

        public async Task<bool> AddUserImage(int id, string imageUrl)
        {
            var result = await _userImageRepository.Add(new UserImage { UserId = id, ImageName = imageUrl });
            await _unitOfWork.SaveChangesAsync();
            return result is not null;
        }

        public async Task<string?> GetUserImagePath(int id)
        {
            var image = await _userImageRepository.GetImageByUser(id);

            return image is null
                ? null
                : image.ImageName;
        }

        public async Task<List<Application.Models.ResponseModels.User>> GetAllUsers()
        {
            return _mapper.Map<List<Application.Models.ResponseModels.User>>(await _userRepository.GetAll());
        }

        public async Task<string?> GetUserEmailByToken(string token)
        {
            var email = await _cache.GetStringAsync(token);

            return email;
        }

        public async Task<bool> SetPassword(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);

            if (user is null) return false;

            var salt = _hasher.GetSalt();

            var hashPassword = _hasher.GetHash(password, salt);

            user!.Salt = salt;
            user.Password = hashPassword;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
