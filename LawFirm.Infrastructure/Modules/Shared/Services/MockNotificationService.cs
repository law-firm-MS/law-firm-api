using System.Threading.Tasks;
using LawFirm.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace LawFirm.Infrastructure.Services
{
    public class MockNotificationService : INotificationService
    {
        private readonly ILogger<MockNotificationService> _logger;

        public MockNotificationService(ILogger<MockNotificationService> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string to, string subject, string body)
        {
            _logger.LogInformation($"[MOCK EMAIL] To: {to}, Subject: {subject}, Body: {body}");
            return Task.CompletedTask;
        }

        public Task SendSmsAsync(string to, string message)
        {
            _logger.LogInformation($"[MOCK SMS] To: {to}, Message: {message}");
            return Task.CompletedTask;
        }
    }
}
