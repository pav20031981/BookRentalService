using BookRentalService.Models;

namespace BookRentalService.Repository
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalHistory>> GetRentalHistoryAsync(int userId);
        Task<int> MarkOverdueRentalsAsync();
        Task<bool> ExtendRentalPeriodAsync(int rentalId, int extensionDays);
        Task<RentalStats> GetRentalStatsAsync();
    }
}
