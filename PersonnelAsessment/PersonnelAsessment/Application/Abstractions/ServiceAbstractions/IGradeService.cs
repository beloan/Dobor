namespace Application.Abstractions.ServiceAbstractions
{
    public interface IGradeService
    {
        public Task<Models.ResponseModels.Grade> AddGrade(Models.RequestModels.Grade grade);
        public Task<Models.ResponseModels.Grade?> GetGrade(int id);
        public Task<Models.ResponseModels.Grade?> UpdateGrade(int id, Models.RequestModels.Grade grade);
        public Task<bool> DeleteGrade(int id);
        public Task<Models.ResponseModels.Grade?> GetGradeByWorkerAndAssigment(int workerId, int assigmentId);
        public Task<List<Models.ResponseModels.Grade>?> GetAllGradesByWorker(int workerId);
    }
}
