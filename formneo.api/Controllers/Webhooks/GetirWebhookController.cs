using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace formneo.api.Controllers.Webhooks
{
    [ApiController]
    [AllowAnonymous]
    [Route("webhooks/getir")] // Örnek: https://domain.com/webhooks/getir/newOrder
    public class GetirWebhookController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GetirWebhookController(IConfiguration configuration)
        {
            _configuration = configuration;
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
        public IActionResult NewOrder([FromBody] object payload)
        {
            if (!IsValidApiKey())
            {
                return Unauthorized(new { message = "Invalid x-api-key" });
            }

            // Şimdilik sadece 200 OK dönüyoruz; ileride iş kuralları eklenecek
            return Ok(new { status = "received", type = "newOrder" });
        }

        [HttpPost("cancelOrder")]
        public IActionResult CancelOrder([FromBody] object payload)
        {
            if (!IsValidApiKey())
            {
                return Unauthorized(new { message = "Invalid x-api-key" });
            }

            // Şimdilik sadece 200 OK dönüyoruz; ileride iş kuralları eklenecek
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
        public IActionResult TestNewOrder([FromBody] object payload)
        {
            if (!IsValidApiKey())
            {
                return Unauthorized(new { message = "Invalid x-api-key" });
            }
            return Ok(new { status = "received", type = "test.newOrder" });
        }

        [HttpPost("test/cancelOrder")]
        public IActionResult TestCancelOrder([FromBody] object payload)
        {
            if (!IsValidApiKey())
            {
                return Unauthorized(new { message = "Invalid x-api-key" });
            }
            return Ok(new { status = "received", type = "test.cancelOrder" });
        }

        // Restoran statü değişiklikleri
        [HttpPost("restaurantStatus")]
        public IActionResult RestaurantStatus([FromBody] object payload)
        {
            if (!IsValidApiKey())
            {
                return Unauthorized(new { message = "Invalid x-api-key" });
            }
            return Ok(new { status = "received", type = "restaurantStatus" });
        }

        // Kurye restoran varışı (arrival)
        [HttpPost("courierArrival")]
        public IActionResult CourierArrival([FromBody] object payload)
        {
            if (!IsValidApiKey())
            {
                return Unauthorized(new { message = "Invalid x-api-key" });
            }
            return Ok(new { status = "received", type = "courierArrival" });
        }
    }
}


