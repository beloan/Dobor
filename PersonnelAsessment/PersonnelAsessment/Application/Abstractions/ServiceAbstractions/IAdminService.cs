namespace Application.Abstractions.ServiceAbstractions
{
    public interface IAdminService
    {
        public Task<Models.ResponseModels.Admin> RegisterAdmin(Models.RequestModels.Admin admin);
        public Task<Models.ResponseModels.Admin> LoginAdmin(Models.RequestModels.Admin admin);
    }
}
