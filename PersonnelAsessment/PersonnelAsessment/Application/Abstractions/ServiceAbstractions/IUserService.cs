namespace Application.Abstractions.ServiceAbstractions
{
    public interface IUserService
    {
        public Task<Models.ResponseModels.User> RegisterUser(Models.RequestModels.User user);
        public Task<Models.ResponseModels.User> LoginUser(Models.RequestModels.User user);
        public Task<Models.ResponseModels.User> ActivateUser(string? email);
        public Task<Models.ResponseModels.User?> GetUserByEmail(string? email);
        public Task<Models.ResponseModels.User?> GetUserById(int id);
        public Task<Models.ResponseModels.User?> UpdateUser(int email, Models.RequestModels.User user);
        public Task<bool> DeleteUser(int id);
        public Task SendConfirmationMessage(string? email);
        //public Task SendConfirmationMessageWithPassword(string? email, string? password);
        public Task AddTokenToCache(string token, string email);
        //public Task<Models.ResponseModels.User> ConfirmEmail(string token);
        public Task<bool> AddUserImage(int id, string imageUrl);
        public Task<string?> GetUserImagePath(int id);
        public Task<List<Models.ResponseModels.User>> GetAllUsers();
        public Task<string?> GetUserEmailByToken(string token);
        public Task<bool> SetPassword(string email, string password);
    }
}
