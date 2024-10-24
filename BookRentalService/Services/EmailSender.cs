using BookRentalService.Repository;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BookRentalService.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _smtpClient;

        public EmailSender(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var mailMessage = new MailMessage("your-email@example.com", to, subject, body);
            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
