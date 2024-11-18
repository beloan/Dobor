using Application.Abstractions.RepositoryAbstractions;
using Application.Abstractions.ServiceAbstractions;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class GradeService : IGradeService
    {
        IGradeRepository _repos;
        IMapper _mapper;
        IUnitOfWork _unitOfWork;

        public GradeService(IGradeRepository gradeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repos = gradeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public async Task<Application.Models.ResponseModels.Grade> AddGrade(Application.Models.RequestModels.Grade grade)
        {
            var result = await _repos.Add(_mapper.Map<Grade>(grade));
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Application.Models.ResponseModels.Grade>(result);
        }

        public async Task<bool> DeleteGrade(int id)
        {
            var result = await _repos.DeleteById(id);
            await _unitOfWork.SaveChangesAsync();

            return result == 1;
        }

        public async Task<List<Application.Models.ResponseModels.Grade>?> GetAllGradesByWorker(int workerId)
        {
            return _mapper.Map<List<Application.Models.ResponseModels.Grade>>((await _repos.GetAll())
                .Where(x => x.WorkerId == workerId));
        }

        public async Task<Application.Models.ResponseModels.Grade?> GetGrade(int id)
        {
            return _mapper.Map<Application.Models.ResponseModels.Grade>(await _repos.GetById(id));
        }

        public async Task<Application.Models.ResponseModels.Grade?> GetGradeByWorkerAndAssigment(int workerId, int assigmentId)
        {
            return _mapper.Map<Application.Models.ResponseModels.Grade>((await _repos.GetAll())
                .FirstOrDefault(x => x.WorkerId == workerId && x.AssigmentId == assigmentId));
        }

        public async Task<Application.Models.ResponseModels.Grade?> UpdateGrade(int id, Application.Models.RequestModels.Grade grade)
        {
            var result = await _repos.UpdateById(id, _mapper.Map<Grade>(grade));
            await _unitOfWork.SaveChangesAsync();

            return result == 1
                ? await GetGrade(id)
                : null;
        }
    }
}
