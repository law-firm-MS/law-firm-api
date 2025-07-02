using LawFirm.Application.Interfaces;

namespace LawFirm.Infrastructure.Services
{
    public class MicrosoftCalendarService : ICalendarService
    {
        public async Task CreateEventAsync(
            string title,
            DateTime start,
            DateTime end,
            string? location = null,
            string? meetingLink = null
        )
        {
            // TODO: Implement Microsoft Graph API event creation here
            await Task.CompletedTask;
        }

        public async Task SyncEventsAsync()
        {
            // TODO: Implement Microsoft Graph API event sync here
            await Task.CompletedTask;
        }
    }
}
