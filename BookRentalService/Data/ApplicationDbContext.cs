using Microsoft.EntityFrameworkCore;
using BookRentalService.Models;

namespace BookRentalService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<OverdueRental> OverdueRentals { get;set; }
        public DbSet<RentalHistory> RentalsHistory { get; set;}
        public DbSet<WaitingList> WaitingLists { get; set; }

    }
}
