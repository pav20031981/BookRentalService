using BookRentalService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookRentalService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(INotificationService notificationService, ILogger<NotificationController> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        // POST: api/notification/send-overdue
        [HttpPost("send-overdue")]
        public async Task<IActionResult> SendOverdueNotifications()
        {
            try
            {
                var success = await _notificationService.SendOverdueNotificationsAsync();
                return success ? Ok("Overdue notifications sent successfully") : BadRequest("Failed to send notifications");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending overdue notifications");
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
