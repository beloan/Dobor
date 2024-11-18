using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using AutoMapper;
using Domain.Entities;
using Domain.EntityProperties;

namespace Infrastructure.Services
{
    public class LeadService : ILeadService
    {
        ILeadRepository _leadRepository;
        IHasherService _hasher;
        IUserService _userService;
        IPasswordGenerator _passwordGenerator;
        IMapper _mapper;
        IUnitOfWork _unitOfWork;
        IOrganizationRepository _organizationRepository;
        IEmailService _emailService;

        public LeadService(ILeadRepository leadRepository, IHasherService hasher, IUserService userService, IPasswordGenerator passwordGenerator, IMapper mapper, IUnitOfWork unitOfWork, IOrganizationRepository organizationRepository, IEmailService emailService)
        {
            _leadRepository = leadRepository;
            _hasher = hasher;
            _userService = userService;
            _passwordGenerator = passwordGenerator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _organizationRepository = organizationRepository;
            _emailService = emailService;
        }

        public async Task<bool> DeleteLead(int id)
        {
            var result = await _leadRepository.DeleteById(id);
            await _unitOfWork.SaveChangesAsync();

            return result == 1;
        }

        public async Task<List<Application.Models.ResponseModels.Lead>?> GetAllLeads()
        {
            return _mapper.Map<List<Application.Models.ResponseModels.Lead>>(await _leadRepository.GetAll());
        }

        public async Task<List<Application.Models.ResponseModels.Lead>?> GetAllLeadsByOrganisation(int organisationId)
        {
            var org = (await _organizationRepository.GetById(organisationId))!;
            if (org is null) return new();

            return _mapper.Map<List<Application.Models.ResponseModels.Lead>>(org.Leads);
        }

        public async Task<Application.Models.ResponseModels.Lead?> GetLeadByEmail(string? email)
        {
            return _mapper.Map<Application.Models.ResponseModels.Lead>(await _leadRepository.GetByEmail(email!));
        }

        public async Task<Application.Models.ResponseModels.Lead?> GetLeadById(int id)
        {
            return _mapper.Map<Application.Models.ResponseModels.Lead>(await _leadRepository.GetById(id));
        }

        public async Task<Application.Models.ResponseModels.Lead> RegisterLead(Application.Models.RequestModels.Lead lead, string? organisationEmail)
        {
            Lead result = null!;
            var realUser = await _leadRepository.GetByEmail(lead.Email!);
            if (realUser is not null)
            {
                var org = await _organizationRepository.GetByEmail(organisationEmail!);
                if (realUser.Organisations is null) realUser.Organisations = new List<Organisation>();
                realUser.Organisations.Add(org!);
                await _unitOfWork.SaveChangesAsync();

                await _emailService.SendEmailAsync($"Вас добавили в организацию {organisationEmail}", lead.Email!, "Добавление в организацию");
            }
            else
            {
                var password = _passwordGenerator.GeneratePassword();

                var userR = _mapper.Map<Lead>(lead);
                userR.Salt = _hasher.GetSalt();
                userR.Password = _hasher.GetHash(password, userR.Salt);
                userR.IsActivated = false;
                userR.Role = Roles.Lead;

                await _userService.SendConfirmationMessage(userR.Email!);

                result = await _leadRepository.Add(userR);
                await _unitOfWork.SaveChangesAsync();

                if(result.Organisations is null) result.Organisations = new List<Organisation>();
                result.Organisations!.Add((await _organizationRepository.GetByEmail(organisationEmail!))!);
                await _unitOfWork.SaveChangesAsync();
            }
            
            return _mapper.Map<Application.Models.ResponseModels.Lead>(result);
        }

        public Task<bool> SendConfirmationMessageWithPassword(string? email, string? password)
        {
            throw new NotImplementedException();
        }

        public async Task<Application.Models.ResponseModels.Lead?> UpdateLead(int id, Application.Models.RequestModels.Lead lead)
        {
            var result = await _leadRepository.UpdateById(id, _mapper.Map<Lead>(lead));
            await _unitOfWork.SaveChangesAsync();

            return result == 1
                ? _mapper.Map<Application.Models.ResponseModels.Lead>(await _leadRepository.GetById(id))
                : null;
        }
    }
}
