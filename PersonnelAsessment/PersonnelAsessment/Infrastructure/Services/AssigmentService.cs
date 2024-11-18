using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class AssigmentService : IAssigmentService
    {
        IAssigmentRepository _repos;
        IMapper _mapper;
        IWorkerService _workerService;
        IGradeService _gradeService;
        IUnitOfWork _unitOfWork;
        ILogger<IAssigmentService> _logger;

        public AssigmentService(IAssigmentRepository assigmentRepository, IMapper mapper, IWorkerService workerService, IGradeService gradeService, IUnitOfWork unitOfWork, ILogger<IAssigmentService> logger)
        {
            _repos = assigmentRepository;
            _mapper = mapper;
            _workerService = workerService;
            _gradeService = gradeService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Application.Models.ResponseModels.Assigment> AddAssigment(Application.Models.RequestModels.Assigment assigment)
        {
            var result = await _repos.Add(_mapper.Map<Assigment>(assigment));

            var workers = await _workerService.GetAllWorkersByForm(result.FormId);
            foreach(var worker in workers!)
            {
                await _gradeService
                    .AddGrade(
                        new Application.Models.RequestModels.Grade
                        {
                            AssigmentId = result.Id
                        });
            }
            _logger.LogInformation("Добавлены пустые записи для оценок", result.Id, result.Form!.Number);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Application.Models.ResponseModels.Assigment>(result);
        }

        public async Task<bool> DeleteAssigment(int id)
        {
            var result = await _repos.DeleteById(id);
            await _unitOfWork.SaveChangesAsync();

            return result == 1;
        }

        public async Task<Application.Models.ResponseModels.Assigment?> GetAssigment(int id)
        {
            return _mapper.Map<Application.Models.ResponseModels.Assigment>(await _repos.GetById(id));
        }

        public async Task<List<Application.Models.ResponseModels.Assigment>?> GetAssigmentByDateAndForm(DateOnly date, int formId)
        {
            return _mapper.Map<List<Application.Models.ResponseModels.Assigment>>((await _repos.GetAll())
                .Where(x => x.Date.Equals(date) && x.FormId == formId).ToList());
        }

        public async Task<List<Application.Models.ResponseModels.Assigment>?> GetAssigmentByForm(int formId)
        {
            return _mapper.Map<List<Application.Models.ResponseModels.Assigment>>((await _repos.GetAll())
                .Where(x => x.FormId == formId).ToList());
        }

        public async Task<List<Application.Models.ResponseModels.Assigment>?> GetAssigmentHeaderByLead(int leadId)
        {
            return _mapper.Map<List<Application.Models.ResponseModels.Assigment>>((await _repos.GetAll())
                .Where(x => x.Id == leadId).DistinctBy(x => new  {x.FormId}).ToList());
        }

        public async Task<Application.Models.ResponseModels.Assigment?> UpdateAssigment(int id, Application.Models.RequestModels.Assigment assigment)
        {
            var result = await _repos.UpdateById(id, _mapper.Map<Assigment>(assigment));
            await _unitOfWork.SaveChangesAsync();

            return result == 1
                ? await GetAssigment(id)
                : null;
        }
    }
}
