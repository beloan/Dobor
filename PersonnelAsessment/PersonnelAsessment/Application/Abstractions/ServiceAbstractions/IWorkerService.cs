namespace Application.Abstractions.ServiceAbstractions
{
    public interface IWorkerService
    {
        public Task<Models.ResponseModels.Worker> RegisterWorker(Models.RequestModels.Worker worker);
        public Task<Models.ResponseModels.Worker?> GetWorkerById(int id);
        public Task<Models.ResponseModels.Worker?> GetWorkerByEmail(string? email);
        public Task<Models.ResponseModels.Worker?> UpdateWorker(int id, Models.RequestModels.Worker worker);
        public Task<bool> DeleteWorker(int id);
        public Task<List<Models.ResponseModels.Worker>?> GetAllWorkers();
        public Task<List<Models.ResponseModels.Worker>?> GetAllWorkersByOrganisation(int organisationId);
        public Task<List<Models.ResponseModels.Worker>?> GetAllWorkersByForm(int formId);
        public Task<bool> SendConfirmationMessageWithPassword(string? email, string? password);

    }
}
