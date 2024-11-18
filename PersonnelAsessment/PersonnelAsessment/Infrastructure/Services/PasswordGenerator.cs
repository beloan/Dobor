using Application.Abstractions.ServiceAbstractions;

namespace Infrastructure.Services
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string? GeneratePassword()
        {
            return "1234qweR";
        }
    }
}
