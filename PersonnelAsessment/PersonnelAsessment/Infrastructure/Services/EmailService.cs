using Application.Abstractions.ServiceAbstractions;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        IConfiguration _config;
        string _host;
        int _port;
        string _serverEmail;
        string _serverPassword;

        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
            _host = _config["Smtp:Server:Host"]!;
            _port = int.Parse(_config["Smtp:Server:Port"]!.ToString());
            _serverEmail = _config["Smtp:Server:Email"]!;
            _serverPassword = _config["Smtp:Server:Password"]!;

        }

        public async Task SendEmailAsync(string message, string to, string subject)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта UDEO", _serverEmail));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_host, _port, true);
                await client.AuthenticateAsync(_serverEmail, _serverPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
