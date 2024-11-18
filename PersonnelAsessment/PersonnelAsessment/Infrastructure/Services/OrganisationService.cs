using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using AutoMapper;
using Domain.Entities;
using Domain.EntityProperties;

namespace Infrastructure.Services
{
    public class OrganisationService : IOrganisationService
    {
        IOrganizationRepository _orgRepository;
        IMapper _mapper;
        IUnitOfWork _unitOfWork;
        IAddressService _addressService;

        public OrganisationService(IAddressService addressService, IOrganizationRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _orgRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _addressService = addressService;
        }

        public async Task<bool> DeleteOrganisation(int id)
        {
            var result = await _orgRepository.DeleteById(id);
            await _unitOfWork.SaveChangesAsync();

            return result == 1;
        }

        public async Task<List<Application.Models.ResponseModels.Organisation>?> GetAllOrganisation()
        {
            return _mapper.Map<List<Application.Models.ResponseModels.Organisation>>(await _orgRepository.GetAll());
        }

        public async Task<Application.Models.ResponseModels.Organisation?> GetOrganisationByEmail(string? email)
        {
            return _mapper.Map<Application.Models.ResponseModels.Organisation>(await _orgRepository.GetByEmail(email!));
        }

        public async Task<Application.Models.ResponseModels.Organisation?> GetOrganisationById(int id)
        {
            return _mapper.Map<Application.Models.ResponseModels.Organisation>(await _orgRepository.GetById(id));
        }

        public async Task<Application.Models.ResponseModels.Organisation> RegisterOrganisation(Application.Models.RequestModels.Organisation organisation)
        {
            if (!await _addressService.CheckAddress(organisation.Address)) throw new Exception("IncorrectAddress");

            var realUser = await _orgRepository.GetByEmail(organisation.Email!);
            if (realUser is not null)
            {
                throw new Exception("Organisation is already exist");
            }

            var userR = _mapper.Map<Organisation>(organisation);
            userR.IsActivated = false;
            userR.Role = Roles.Organisation;

            var result = await _orgRepository.Add(userR);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Application.Models.ResponseModels.Organisation>(result);
        }

        public Task<bool> SendConfirmationMessage(string? email)
        {
            throw new NotImplementedException();
        }

        public async Task<Application.Models.ResponseModels.Organisation?> UpdateOrganisation(int id, Application.Models.RequestModels.Organisation organisation)
        {
            var result = await _orgRepository.UpdateById(id, _mapper.Map<Organisation>(organisation));
            await _unitOfWork.SaveChangesAsync();

            return result == 1
                ? await GetOrganisationById(id)
                : null;
        }
    }
}
