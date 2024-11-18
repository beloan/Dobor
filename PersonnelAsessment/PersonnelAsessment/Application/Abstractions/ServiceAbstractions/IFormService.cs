namespace Application.Abstractions.ServiceAbstractions
{
    public interface IFormService
    {
        public Task<Models.ResponseModels.Form> AddForm(Models.RequestModels.Form form, int organisationId);
        public Task<Models.ResponseModels.Form?> GetForm(int id);
        public Task<Models.ResponseModels.Form?> UpdateForm(int id, Models.RequestModels.Form form);
        public Task<bool> DeleteForm(int id);
        public Task<List<Models.ResponseModels.Form>?> GetAllFormsByLead(int leadId);
        public Task<List<Models.ResponseModels.Form>?> GetAllFormsByOrganisation(int organisationId);
        public Task<List<Models.ResponseModels.Form>?> GetAllFormsByLeadAndOrganisation(int leadId, int organisationId);
        public Task<List<Models.ResponseModels.Form>?> GetAllForms();
    }
}
