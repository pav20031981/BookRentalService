using BookRentalService.Models;

namespace BookRentalService.Repository
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> SearchBooksAsync(string name, string genre);
        Task<Book> GetBookByIdAsync(int bookId);
        Task<bool> RentBookAsync(int bookId, int userId);
        Task<bool> ReturnBookAsync(int rentalId);
    }
}
