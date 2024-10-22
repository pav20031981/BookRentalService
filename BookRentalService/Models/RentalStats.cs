namespace BookRentalService.Models
{
    public class RentalStats
    {
        public Book MostOverdueBook { get; set; }
        public Book MostPopularBook { get; set; }
        public Book LeastPopularBook { get; set; }
        public int TotalOverdueBooks { get; set; }
        public int TotalRentals { get; set; }
    }
}
