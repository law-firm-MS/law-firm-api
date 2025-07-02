using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using LawFirm.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LawFirm.Infrastructure.Services
{
    public class SmtpEmailService : INotificationService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SmtpEmailService> _logger;

        public SmtpEmailService(IConfiguration config, ILogger<SmtpEmailService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpSection = _config.GetSection("SmtpSettings");
            var host = smtpSection["Host"];
            var port = int.Parse(smtpSection["Port"]);
            var username = smtpSection["Username"];
            var password = smtpSection["Password"];
            var from = smtpSection["From"];
            var enableSsl = bool.Parse(smtpSection["EnableSsl"] ?? "true");

            using var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = enableSsl,
            };
            var mail = new MailMessage(from, to, subject, body);
            try
            {
                await client.SendMailAsync(mail);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "SMTP send failed");
                throw;
            }
        }

        public Task SendSmsAsync(string to, string message)
        {
            _logger.LogInformation($"[SMTP SMS MOCK] To: {to}, Message: {message}");
            return Task.CompletedTask;
        }
    }
}
