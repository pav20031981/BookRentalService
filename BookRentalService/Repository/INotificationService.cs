namespace BookRentalService.Repository
{
    public interface INotificationService
    {
        Task<bool> SendOverdueNotificationsAsync();
    }
}
