namespace Application.Abstractions.ServiceAbstractions
{
    public interface IAssigmentService
    {
        public Task<Models.ResponseModels.Assigment> AddAssigment(Models.RequestModels.Assigment assigment);
        public Task<Models.ResponseModels.Assigment?> GetAssigment(int id);
        public Task<Models.ResponseModels.Assigment?> UpdateAssigment(int id, Models.RequestModels.Assigment assigment);
        public Task<bool> DeleteAssigment(int id);
        public Task<List<Models.ResponseModels.Assigment>?> GetAssigmentHeaderByLead(int leadId);
        public Task<List<Models.ResponseModels.Assigment>?> GetAssigmentByForm(int formId);
        public Task<List<Models.ResponseModels.Assigment>?> GetAssigmentByDateAndForm(DateOnly date, int formId);
    }
}
