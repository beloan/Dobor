namespace Application.Abstractions.ServiceAbstractions
{
    public interface IOrganisationService
    {
        public Task<Models.ResponseModels.Organisation> RegisterOrganisation(Models.RequestModels.Organisation organisation);
        public Task<Models.ResponseModels.Organisation?> GetOrganisationByEmail(string? email);
        public Task<Models.ResponseModels.Organisation?> GetOrganisationById(int id);
        public Task<Models.ResponseModels.Organisation?> UpdateOrganisation(int id, Models.RequestModels.Organisation organisation);
        public Task<bool> DeleteOrganisation(int id);
        public Task<List<Models.ResponseModels.Organisation>?> GetAllOrganisation();
        public Task<bool> SendConfirmationMessage(string? email);
    }
}
