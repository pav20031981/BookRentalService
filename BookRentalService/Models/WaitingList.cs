namespace BookRentalService.Models
{
    public class WaitingList
    {
        public int WaitingListId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime AddedToQueueDate { get; set; }
        public bool IsNotified { get; set; } // Tracks if the user was notified when the book became available
    }
}
