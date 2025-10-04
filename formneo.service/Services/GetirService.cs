using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using formneo.core.Configuration;
using formneo.core.DTOs.Getir;
using formneo.core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;

namespace formneo.service.Services
{
    public class GetirService : IGetirService
    {
        private readonly ILogger<GetirService> _logger;
        private readonly GetirOptions _options;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;
        private readonly IMailService _mailService;

        public GetirService(ILogger<GetirService> logger, IOptions<GetirOptions> options, IHttpClientFactory httpClientFactory, IMemoryCache cache, IMailService mailService)
        {
            _logger = logger;
            _options = options.Value;
            _httpClientFactory = httpClientFactory;
            _cache = cache;
            _mailService = mailService;
        }

        public Task HandleNewOrderAsync(string payloadJson)
        {
            _logger.LogInformation("Getir newOrder received: {Payload}", Truncate(payloadJson));
            try
            {
                var recipients = _options.NotificationEmails?.Count > 0 ? _options.NotificationEmails.ToArray() : new[] { "muratmerdogan@gmail.com" };
                var subject = "Getir - Yeni Sipariş Webhook";
                var body = $"<pre>{System.Net.WebUtility.HtmlEncode(Truncate(payloadJson))}</pre>";
                _ = _mailService.SendEmailAsync(recipients, subject, body, true);
            }
            catch { /* email hatası ana akışı bozmasın */ }
            // TODO: Map payload to domain models and persist
            return Task.CompletedTask;
        }

        public Task HandleCancelOrderAsync(string payloadJson)
        {
            _logger.LogInformation("Getir cancelOrder received: {Payload}", Truncate(payloadJson));
            // TODO: Apply business logic for cancellation
            return Task.CompletedTask;
        }

        public Task HandleRestaurantStatusAsync(string payloadJson)
        {
            _logger.LogInformation("Getir restaurantStatus received: {Payload}", Truncate(payloadJson));
            return Task.CompletedTask;
        }

        public Task HandleCourierArrivalAsync(string payloadJson)
        {
            _logger.LogInformation("Getir courierArrival received: {Payload}", Truncate(payloadJson));
            return Task.CompletedTask;
        }

        private static string Truncate(string text, int max = 4000)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return text.Length <= max ? text : text.Substring(0, max);
        }

        public async Task<GetirPosStatusResponse?> GetPosStatusAsync(CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient();
            var url = CombineUrl(_options.ApiBaseUrl, "/restaurants/pos-status");
            var body = new GetirPosStatusPostRequest
            {
                appSecretKey = _options.AppSecretKey ?? string.Empty,
                restaurantSecretKey = _options.RestaurantSecretKey ?? string.Empty
            };
            var bodyJson = JsonSerializer.Serialize(body);
            using var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(bodyJson, Encoding.UTF8, "application/json")
            };

            using var resp = await client.SendAsync(req, cancellationToken);
            var content = await resp.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation("Getir POST pos-status (status) {Status}: {Body}", (int)resp.StatusCode, Truncate(content));
            resp.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<GetirPosStatusResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<GetirPosStatusResponse?> SetPosStatusAsync(GetirSetPosStatusRequest request, CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient();
            var url = CombineUrl(_options.ApiBaseUrl, "/restaurants/pos-status");
            // Body yalnizca posStatus, appSecretKey ve restaurantSecretKey icermeli
            var body = new
            {
                posStatus = request.PosStatus,
                appSecretKey = _options.AppSecretKey ?? string.Empty,
                restaurantSecretKey = _options.RestaurantSecretKey ?? string.Empty
            };
            var bodyJson = JsonSerializer.Serialize(body);
            using var req = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(bodyJson, Encoding.UTF8, "application/json")
            };
            // Header eklemiyoruz; dokümana göre body ile doğrulanır

            using var resp = await client.SendAsync(req, cancellationToken);
            var content = await resp.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation("Getir PUT pos-status {Status}: {Body}", (int)resp.StatusCode, Truncate(content));
            resp.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<GetirPosStatusResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<GetirPosStatusResponse?> PostPosStatusAuthAsync(GetirPosStatusPostRequest request, CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient();
            var url = CombineUrl(_options.ApiBaseUrl, "/restaurants/pos-status");
            var bodyJson = JsonSerializer.Serialize(request);
            using var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(bodyJson, Encoding.UTF8, "application/json")
            };
            // Bu endpoint dokümana göre yalnızca body ile appSecretKey ve restaurantSecretKey alır
            // Header'ları eklemiyoruz; sadece content-type

            using var resp = await client.SendAsync(req, cancellationToken);
            var content = await resp.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation("Getir POST pos-status AUTH {Status}: {Body}", (int)resp.StatusCode, Truncate(content));
            resp.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<GetirPosStatusResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private void ApplyAuthHeaders(HttpRequestMessage req, string? body = null)
        {
            if (!string.IsNullOrWhiteSpace(_options.AppSecretKey))
                req.Headers.Add("X-App-Secret-Key", _options.AppSecretKey);
            if (!string.IsNullOrWhiteSpace(_options.RestaurantSecretKey))
                req.Headers.Add("X-Restaurant-Secret-Key", _options.RestaurantSecretKey);
            if (!string.IsNullOrWhiteSpace(_options.RestaurantId))
                req.Headers.Add("X-Restaurant-Id", _options.RestaurantId);

            // Bazı Getir endpoint'leri gövde imzası isteyebilir; dokümana göre uyarlanır
            if (!string.IsNullOrEmpty(body) && !string.IsNullOrWhiteSpace(_options.RestaurantSecretKey))
            {
                var signature = ComputeSha256(body + _options.RestaurantSecretKey);
                req.Headers.Add("X-Body-Signature", signature);
            }
        }

        private static string ComputeSha256(string text)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(text);
            var hash = sha.ComputeHash(bytes);
            var sb = new StringBuilder(hash.Length * 2);
            foreach (var b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        private static string CombineUrl(string? baseUrl, string path)
        {
            if (string.IsNullOrWhiteSpace(baseUrl)) return path;
            return baseUrl!.TrimEnd('/') + "/" + path.TrimStart('/');
        }

        public async Task<GetirAuthLoginResponse?> AuthLoginAsync(GetirAuthLoginRequest request, CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient();
            var url = CombineUrl(_options.ApiBaseUrl, "/auth/login");
            var bodyJson = JsonSerializer.Serialize(request);
            using var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(bodyJson, Encoding.UTF8, "application/json")
            };

            using var resp = await client.SendAsync(req, cancellationToken);
            var content = await resp.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation("Getir auth/login {Status}: {Body}", (int)resp.StatusCode, Truncate(content));
            resp.EnsureSuccessStatusCode();
            var data = JsonSerializer.Deserialize<GetirAuthLoginResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!string.IsNullOrWhiteSpace(data?.token))
            {
                _cache.Set("getir_token", data.token, TimeSpan.FromHours(6));
            }
            return data;
        }

        public async Task<List<GetirPaymentMethodItem>?> GetPaymentMethodsAsync(CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient();
            var url = CombineUrl(_options.ApiBaseUrl, "/payment-methods");
            using var req = new HttpRequestMessage(HttpMethod.Get, url);
            if (_cache.TryGetValue<string>("getir_token", out var token) && !string.IsNullOrWhiteSpace(token))
            {
                req.Headers.Add("token", token);
            }

            using var resp = await client.SendAsync(req, cancellationToken);
            var content = await resp.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation("Getir payment-methods {Status}: {Body}", (int)resp.StatusCode, Truncate(content));
            resp.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<List<GetirPaymentMethodItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}


