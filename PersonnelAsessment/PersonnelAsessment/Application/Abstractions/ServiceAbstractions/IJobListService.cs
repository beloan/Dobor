namespace Application.Abstractions.ServiceAbstractions
{
    public interface IJobListService
    {
        public Task<Models.ResponseModels.JobList> AddJobList(Models.RequestModels.JobList jobList);
        public Task<Models.ResponseModels.JobList?> GetJobList(int id);
        public Task<Models.ResponseModels.JobList?> UpdateJobList(int id, Models.RequestModels.JobList jobList);
        public Task<bool> DeleteJobList(int id);
        public Task<List<Models.ResponseModels.JobList>?> GetAllJobListsByForm(int formId);
        public Task<List<Models.ResponseModels.JobList>?> GetAllJobListsByFormAndDay(string day, int formId);
        public Task<string?> GetHtmlTable(int organisationId);
        public Task<string?> GetHtmlTable(string? email);
    }
}
