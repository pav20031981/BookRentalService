using BookRentalService.Models;
using BookRentalService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookRentalService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly ILogger<RentalController> _logger;

        public RentalController(IRentalService rentalService, ILogger<RentalController> logger)
        {
            _rentalService = rentalService;
            _logger = logger;
        }

        // GET: api/rental/history/{userId}
        [HttpGet("history/{userId}")]
        public async Task<IActionResult> GetRentalHistory(int userId)
        {
            try
            {
                var history = await _rentalService.GetRentalHistoryAsync(userId);
                return Ok(history);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving rental history");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/rental/mark-overdue
        [HttpPost("mark-overdue")]
        public async Task<IActionResult> MarkOverdueRentals()
        {
            try
            {
                var overdueCount = await _rentalService.MarkOverdueRentalsAsync();
                return Ok($"{overdueCount} rentals marked as overdue");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while marking overdue rentals");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/rental/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetRentalStats()
        {
            try
            {
                var stats = await _rentalService.GetRentalStatsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving rental stats");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/rental/extend
        [HttpPut("extend")]
        public async Task<IActionResult> ExtendRental([FromBody] ExtendRentalRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var success = await _rentalService.ExtendRentalPeriodAsync(request.RentalId, request.ExtensionDays);
                return success ? Ok("Rental period extended successfully") : BadRequest("Failed to extend the rental period");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while extending rental");
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
