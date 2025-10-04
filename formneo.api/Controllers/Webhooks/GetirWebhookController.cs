using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Threading.Tasks;
using formneo.core.Services;

namespace formneo.api.Controllers.Webhooks
{
    [ApiController]
    [AllowAnonymous]
    [Route("webhooks/getir")] // Örnek: https://domain.com/webhooks/getir/newOrder
    public class GetirWebhookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IGetirService _getirService;

        public GetirWebhookController(IConfiguration configuration, IGetirService getirService)
        {
            _configuration = configuration;
            _getirService = getirService;
        }

        private bool IsValidApiKey()
        {
            var configuredKey = _configuration["Getir:WebhookApiKey"];
            if (string.IsNullOrWhiteSpace(configuredKey))
            {
                // Konfigürasyon girilmemişse development kolaylığı: her çağrıyı kabul ETMEYELİM; 401 dönelim
                return false;
            }

            if (!Request.Headers.TryGetValue("x-api-key", out var headerValues))
            {
                return false;
            }

            var providedKey = headerValues.ToString();
            return string.Equals(providedKey, configuredKey, System.StringComparison.Ordinal);
        }

        [HttpPost("newOrder")]
        public async Task<IActionResult> NewOrder([FromBody] object payload)
        {
            if (!IsValidApiKey())
            {
                return Unauthorized(new { message = "Invalid x-api-key" });
            }

            await _getirService.HandleNewOrderAsync(payload?.ToString() ?? "{}");
            return Ok(new { status = "received", type = "newOrder" });
        }

        [HttpPost("cancelOrder")]
        public async Task<IActionResult> CancelOrder([FromBody] object payload)
        {
            if (!IsValidApiKey())
            {
                return Unauthorized(new { message = "Invalid x-api-key" });
            }

            await _getirService.HandleCancelOrderAsync(payload?.ToString() ?? "{}");
            return Ok(new { status = "received", type = "cancelOrder" });
        }

        // Basit sağlık kontrolü (x-api-key gerektirmez)
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { status = "ok", service = "getir-webhook", timeUtc = DateTime.UtcNow });
        }

        // Test ortamı için uçlar
        [HttpPost("test/newOrder")]
        public async Task<IActionResult> TestNewOrder([FromBody] object payload)
        {
            if (!IsValidApiKey())
            {
                return Unauthorized(new { message = "Invalid x-api-key" });
            }
            await _getirService.HandleNewOrderAsync(payload?.ToString() ?? "{}");
            return Ok(new { status = "received", type = "test.newOrder" });
        }

        [HttpPost("test/cancelOrder")]
        public async Task<IActionResult> TestCancelOrder([FromBody] object payload)
        {
            if (!IsValidApiKey())
            {
                return Unauthorized(new { message = "Invalid x-api-key" });
            }
            await _getirService.HandleCancelOrderAsync(payload?.ToString() ?? "{}");
            return Ok(new { status = "received", type = "test.cancelOrder" });
        }

        // Restoran statü değişiklikleri
        [HttpPost("restaurantStatus")]
        public async Task<IActionResult> RestaurantStatus([FromBody] object payload)
        {
            if (!IsValidApiKey())
            {
                return Unauthorized(new { message = "Invalid x-api-key" });
            }
            await _getirService.HandleRestaurantStatusAsync(payload?.ToString() ?? "{}");
            return Ok(new { status = "received", type = "restaurantStatus" });
        }

        // Kurye restoran varışı (arrival)
        [HttpPost("courierArrival")]
        public async Task<IActionResult> CourierArrival([FromBody] object payload)
        {
            if (!IsValidApiKey())
            {
                return Unauthorized(new { message = "Invalid x-api-key" });
            }
            await _getirService.HandleCourierArrivalAsync(payload?.ToString() ?? "{}");
            return Ok(new { status = "received", type = "courierArrival" });
        }
    }
}


