namespace Application.Abstractions.ServiceAbstractions
{
    public interface ILeadService
    {
        public Task<Models.ResponseModels.Lead> RegisterLead(Models.RequestModels.Lead lead, string? organisationEmail);
        public Task<Models.ResponseModels.Lead?> GetLeadById(int id);
        public Task<Models.ResponseModels.Lead?> GetLeadByEmail(string? email);
        public Task<List<Models.ResponseModels.Lead>?> GetAllLeadsByOrganisation(int organisationId);
        public Task<List<Models.ResponseModels.Lead>?> GetAllLeads();
        public Task<Models.ResponseModels.Lead?> UpdateLead(int id, Models.RequestModels.Lead lead);
        public Task<bool> DeleteLead(int id);
        public Task<bool> SendConfirmationMessageWithPassword(string? email, string? password);
    }
}
