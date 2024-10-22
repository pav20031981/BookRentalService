using System;

namespace BookRentalService.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public bool IsOverdue { get; set; }
        public bool NotificationSent { get; set; }
    }

    public class RentBookRequest
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }

    public class ReturnBookRequest
    {
        public int RentalId { get; set; }
    }

    public class ExtendRentalRequest
    {
        public int RentalId { get; set; }
        public int ExtensionDays { get; set; }
    }
}
