using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using AutoMapper;
using Domain.Entities;
using Domain.EntityProperties;

namespace Infrastructure.Services
{
    public class WorkerService : IWorkerService
    {
        IWorkerRepository _repos;
        IMapper _mapper;
        IPasswordGenerator _passwordGenerator;
        IHasherService _hasher;
        IUserService _userService;
        IUnitOfWork _unitOfWork;
        IEmailService _emailService;

        public WorkerService(IWorkerRepository workerRepository, IMapper mapper, IPasswordGenerator passwordGenerator, IHasherService hasher, IUserService userService, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _repos = workerRepository;
            _mapper = mapper;
            _passwordGenerator = passwordGenerator;
            _hasher = hasher;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<bool> DeleteWorker(int id)
        {
            var result = await _repos.DeleteById(id);
            await _unitOfWork.SaveChangesAsync();

            return result == 1;
        }

        public async Task<List<Application.Models.ResponseModels.Worker>?> GetAllWorkers()
        {
            return _mapper.Map<List<Application.Models.ResponseModels.Worker>>(await _repos.GetAll());
        }

        public async Task<List<Application.Models.ResponseModels.Worker>?> GetAllWorkersByForm(int formId)
        {
            var workers = (await _repos.GetAll())
                .Where(x => x.FormId == formId).ToList();

            workers.Sort((a, b) =>
            {
                if (a.FormId == b.FormId) return 0;
                else if (a.Form!.Number > b.Form!.Number) return 1;
                else return -1;
            });
            return _mapper.Map<List<Application.Models.ResponseModels.Worker>>(workers);
        }

        public async Task<List<Application.Models.ResponseModels.Worker>?> GetAllWorkersByOrganisation(int organisationId)
        {
            return _mapper.Map<List<Application.Models.ResponseModels.Worker>>((await _repos.GetAll())
                .Where(x => x.Form!.OrganisationId == organisationId).ToList());
        }

        public async Task<Application.Models.ResponseModels.Worker?> GetWorkerByEmail(string? email)
        {
            return _mapper.Map<Application.Models.ResponseModels.Worker>(await _repos.GetByEmail(email!));
        }

        public async Task<Application.Models.ResponseModels.Worker?> GetWorkerById(int id)
        {
            return _mapper.Map<Application.Models.ResponseModels.Worker>(await _repos.GetById(id));
        }

        public async Task<Application.Models.ResponseModels.Worker> RegisterWorker(Application.Models.RequestModels.Worker worker)
        {
            var realUser = await _repos.GetByEmail(worker.Email!);
            if (realUser is not null)
            {
                throw new Exception("Teacher is already exist");
            }

            var userR = _mapper.Map<Worker>(worker);
            userR.IsActivated = false;
            userR.Role = Roles.Worker;

            await _userService.SendConfirmationMessage(userR.Email!);

            var result = await _repos.Add(userR);
            await _unitOfWork.SaveChangesAsync();

            var teacherEmail = result.Form!.TeamLead!.Email;
            await _emailService
                .SendEmailAsync($"В ваш team" +
                $"{result.Form.Number}({result.Form.Organisation!.Name})" +
                $"был добавлен worker {result.FIO}({result.Email})",
                teacherEmail!, "Новый worker");

            return _mapper.Map<Application.Models.ResponseModels.Worker>(result);
        }

        public Task<bool> SendConfirmationMessageWithPassword(string? email, string? password)
        {
            throw new NotImplementedException();
        }

        public async Task<Application.Models.ResponseModels.Worker?> UpdateWorker(int id, Application.Models.RequestModels.Worker worker)
        {
            var result = await _repos.UpdateById(id, _mapper.Map<Worker>(worker));
            await _unitOfWork.SaveChangesAsync();

            return result == 1
                ? _mapper.Map<Application.Models.ResponseModels.Worker>(await _repos.GetById(id))
                : null;
        }
    }
}
