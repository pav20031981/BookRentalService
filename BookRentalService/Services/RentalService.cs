using BookRentalService.Data;
using BookRentalService.Models;
using BookRentalService.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookRentalService.Services
{
    public class RentalService : IRentalService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggerService<RentalService> _logger;

        public RentalService(ApplicationDbContext context, ILoggerService<RentalService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // View rental history by user
        public async Task<IEnumerable<RentalHistory>> GetRentalHistoryAsync(int userId)
        {
            try
            {
                return await _context.Rentals
                                     .Where(r => r.UserId == userId)
                                     .Include(r => r.Book)
                                     .Select(r => new RentalHistory
                                     {
                                         BookTitle = r.Book.Title,
                                         RentalDate = r.RentalDate,
                                         ReturnDate = r.ReturnDate,
                                         DueDate = r.DueDate
                                     })
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving rental history for user {UserId}", userId);
                throw new Exception("Error retrieving rental history.");
            }
        }

        // Mark overdue rentals
        public async Task<int> MarkOverdueRentalsAsync()
        {
            try
            {
                var overdueRentals = await _context.Rentals
                    .Where(r => !r.ReturnDate.HasValue && r.DueDate < DateTime.UtcNow)
                    .ToListAsync();

                foreach (var rental in overdueRentals)
                {
                    rental.IsOverdue = true;
                    _context.Rentals.Update(rental);
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("{Count} rentals marked as overdue.", overdueRentals.Count);
                return overdueRentals.Count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking overdue rentals.");
                throw new Exception("Error marking overdue rentals.");
            }
        }

        // Get rental statistics
        public async Task<RentalStats> GetRentalStatsAsync()
        {
            try
            {
                var mostOverdueBook = await _context.Rentals
                    .Where(r => r.IsOverdue)
                    .GroupBy(r => r.Book)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefaultAsync();

                var mostPopularBook = await _context.Rentals
                    .GroupBy(r => r.Book)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefaultAsync();

                var leastPopularBook = await _context.Rentals
                    .GroupBy(r => r.Book)
                    .OrderBy(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefaultAsync();

                var totalRentals = await _context.Rentals.CountAsync();
                var totalOverdueBooks = await _context.Rentals.CountAsync(r => r.IsOverdue);

                return new RentalStats
                {
                    MostOverdueBook = mostOverdueBook,
                    MostPopularBook = mostPopularBook,
                    LeastPopularBook = leastPopularBook,
                    TotalRentals = totalRentals,
                    TotalOverdueBooks = totalOverdueBooks
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving rental statistics.");
                throw new Exception("Error retrieving rental statistics.");
            }
        }

        // Extend rental period
        public async Task<bool> ExtendRentalPeriodAsync(int rentalId, int extensionDays)
        {
            try
            {
                var rental = await _context.Rentals.FindAsync(rentalId);
                if (rental == null || rental.ReturnDate.HasValue)
                    throw new Exception("Invalid rental or rental already returned.");

                rental.DueDate = rental.DueDate.AddDays(extensionDays);
                _context.Rentals.Update(rental);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Rental {RentalId} extended by {Days} days", rentalId, extensionDays);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while extending rental {RentalId}", rentalId);
                throw new Exception("Error extending the rental.");
            }
        }
    }

}
