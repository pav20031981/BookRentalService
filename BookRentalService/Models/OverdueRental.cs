namespace BookRentalService.Models
{
    public class OverdueRental
    {
        public int OverdueRentalId { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
        public DateTime OverdueDate { get; set; }
        public bool NotificationSent { get; set; } // Tracks if the notification email was sent
    }
}
