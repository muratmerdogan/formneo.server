using System.Collections.Generic;

namespace formneo.core.Configuration
{
    public class GetirOptions
    {
        public string WebhookApiKey { get; set; } = string.Empty;
        public string? ApiBaseUrl { get; set; }
        public string? MerchantId { get; set; }
        public string? Secret { get; set; }
        public string? AppSecretKey { get; set; }
        public string? RestaurantSecretKey { get; set; }
        public string? RestaurantId { get; set; }
        public List<string> NotificationEmails { get; set; } = new List<string>();
    }
}


