namespace LawFirm.Application.Interfaces
{
    public interface ICalendarService
    {
        Task CreateEventAsync(
            string title,
            DateTime start,
            DateTime end,
            string? location = null,
            string? meetingLink = null
        );
        Task SyncEventsAsync();
    }
}
