namespace Application.Abstractions.ServiceAbstractions
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string message, string to, string subject);
    }
}
