namespace BookRentalService.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
        public string EmailAddress { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsSuccessful { get; set; } // Tracks if the email was successfully sent
    }
}
