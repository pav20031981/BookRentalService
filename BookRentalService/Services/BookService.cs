using BookRentalService.Data;
using BookRentalService.Models;
using BookRentalService.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookRentalService.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        //private readonly ILoggerService<BookService> _logger;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
            //_logger = logger;
        }

        // Search for books by name and/or genre
        public async Task<IEnumerable<Book>> SearchBooksAsync(string name, string genre)
        {
            try
            {
                var query = _context.Books.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                    query = query.Where(b => b.Title.Contains(name));

                if (!string.IsNullOrEmpty(genre))
                    query = query.Where(b => b.Genre == genre);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error while searching for books");
                throw new Exception("Error searching for books.");
            }
        }

        // Rent a book by user
        public async Task<bool> RentBookAsync(int bookId, int userId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var book = await _context.Books.FindAsync(bookId);

                    if (book == null)
                        throw new Exception("Book not found");

                    if (book.IsRented)
                        throw new Exception("Book is already rented");

                    book.IsRented = true;

                    var rental = new Rental
                    {
                        BookId = bookId,
                        UserId = userId,
                        RentalDate = DateTime.UtcNow,
                        DueDate = DateTime.UtcNow.AddDays(14)
                    };

                    _context.Rentals.Add(rental);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    //_logger.LogInformation("Book {BookId} rented by User {UserId}", bookId, userId);
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    //_logger.LogError(ex, "Error while renting book {BookId} for user {UserId}", bookId, userId);
                    throw new Exception("Error while renting the book.");
                }
            }
        }

        // Return a book
        public async Task<bool> ReturnBookAsync(int rentalId)
        {
            try
            {
                var rental = await _context.Rentals.Include(r => r.Book).FirstOrDefaultAsync(r => r.RentalId == rentalId);

                if (rental == null)
                    throw new Exception("Rental record not found");

                rental.Book.IsRented = false;
                rental.ReturnDate = DateTime.UtcNow;

                _context.Rentals.Update(rental);
                await _context.SaveChangesAsync();

                //_logger.LogInformation("Book {BookId} returned by User {UserId}", rental.BookId, rental.UserId);
                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error while returning book with rental id {RentalId}", rentalId);
                throw new Exception("Error while returning the book.");
            }
        }

        // Get book by ID
        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            try
            {
                return await _context.Books.FindAsync(bookId) ?? throw new Exception("Book not found");
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error while retrieving book {BookId}", bookId);
                throw new Exception("Error retrieving the book.");
            }
        }
    }

}
