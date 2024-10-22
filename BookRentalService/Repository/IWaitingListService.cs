namespace BookRentalService.Repository
{
    public interface IWaitingListService
    {
        Task<bool> AddToWaitingListAsync(int bookId, int userId);
        Task NotifyUsersWhenBookAvailable(int bookId);
    }
}
