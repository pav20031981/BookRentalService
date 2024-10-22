namespace BookRentalService.Models
{
    public class RentalHistory
    {
        public string BookTitle { get; set; }
        public int RentalHistoryId { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsOverdue { get; set; }
    }
}
