namespace BookRentalService.Repository
{
    public interface IBookReservationService
    {
        Task<bool> ReserveBookAsync(int bookId, int userId);
        Task NotifyWhenBookReservedAsync(int bookId);
    }
}
