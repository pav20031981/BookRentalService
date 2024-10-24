using BookRentalService.Data;
using BookRentalService.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookRentalService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        //private readonly IEmailSender _emailSender;
        //private readonly ILoggerService<NotificationService> _logger;

        public NotificationService(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            //_emailSender = emailSender;
            //_logger = logger;
        }

        // Send overdue notifications
        public async Task<bool> SendOverdueNotificationsAsync()
        {
            try
            {
                //var overdueRentals = await _context.Rentals
                //    .Where(r => r.IsOverdue && !r.NotificationSent)
                //    .Include(r => r.User)
                //    .Include(r => r.Book)
                //    .ToListAsync();

                //foreach (var rental in overdueRentals)
                //{
                //    var emailSubject = "Overdue Book Notification";
                //    var emailBody = $"Dear {rental.User.FirstName},\n\nYour rental of '{rental.Book.Title}' is overdue. Please return it as soon as possible.";
                //    await _emailSender.SendEmailAsync(rental.User.Email, emailSubject, emailBody);

                //    rental.NotificationSent = true;
                //    _context.Rentals.Update(rental);
                //}

                //await _context.SaveChangesAsync();
                ////_logger.LogInformation("{Count} overdue notifications sent.", overdueRentals.Count);
                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error sending overdue notifications.");
                throw new Exception("Error sending overdue notifications.");
            }
        }
    }

}
