using Newtonsoft.Json;
using QuestDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs.PCTrack;
using formneo.core.Models.PCTracking;
using formneo.core.Services;

namespace formneo.service.Services
{
    public class QuestDBService
    {
        private readonly HttpClient _httpClient;
        private const string QuestDbUrl = "http://10.10.27.108:9000/api/v2/write";

        public QuestDBService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendLoginEventToQuestDB(PCTrackDto dto)
        {
            try
            {
                // TCP protokolü ile bağlantı (varsayılan port 9009)
                using var sender = Sender.New("tcp::addr=10.10.27.108:9009;");

                sender.Table("PCTrack")
                    .Symbol("PCname", dto.PCname ?? "UnknownPC")
                    .Column("ProcessType", dto.ProcessType.HasValue ? (int)dto.ProcessType.Value : 0)
                    .Column("LoginType", dto.LoginType.HasValue ? (int)dto.LoginType.Value : 0)
                    .Column("LoginProcessName", dto.LoginProcessName ?? "")
                    .Column("LoginId", dto.LoginId ?? "")
                    .Column("SubjectLoginId", dto.SubjectLoginId ?? "")
                    .AtAsync(DateTime.SpecifyKind(dto.ProcessTime ?? DateTime.UtcNow, DateTimeKind.Utc));

                await sender.SendAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[QuestDB TCP] Hata: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> GetLastLoginInfoAsync(string pcName, DateTime processDate)
        {
            // processDate'i QuestDB'nin desteklediği timestamp formatına çevir (örneğin ISO 8601)
            DateTime from = processDate.AddSeconds(-1); // 1 saniye öncesi
            DateTime to = processDate.AddSeconds(1);    // 1 saniye sonrası

            string fromStr = from.ToString("yyyy-MM-ddTHH:mm:ss");
            string toStr = to.ToString("yyyy-MM-ddTHH:mm:ss");

            string query = $@"
SELECT *
FROM PCTrack
WHERE PCname = '{pcName}'
  AND ProcessTime BETWEEN '{fromStr}' AND '{toStr}'
LIMIT 1";

            string url = $"http://10.10.27.108:9000/exec?query={Uri.EscapeDataString(query)}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"[QuestDB SELECT] Hata: {response.StatusCode}");
                return false;
            }

            var json = await response.Content.ReadAsStringAsync();
            var parsed = JsonConvert.DeserializeObject<QuestDbSelectResponse>(json);

            // Kayıt varsa true, yoksa false döndür
            return parsed.dataset.Length > 0;
        }
        public async Task<DateTime?> GetLastProcessTimeByPcNameAsync(string pcName)
        {
            string query = $@"
    SELECT ProcessTime 
    FROM PCTrack 
    WHERE PCname = '{pcName}' 
    ORDER BY ProcessTime DESC 
    LIMIT 1";

            string url = $"http://10.10.27.108:9000/exec?query={Uri.EscapeDataString(query)}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"[QuestDB SELECT] Hata: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var parsed = JsonConvert.DeserializeObject<QuestDbSelectResponse>(json);

            if (parsed?.dataset == null || parsed.dataset.Length == 0 || parsed.dataset[0].Length == 0)
                return null;

            var processTimeStr = parsed.dataset[0][0];

            if (DateTimeOffset.TryParse(processTimeStr, out var dto))
                return dto.UtcDateTime.ToLocalTime(); // Yerel saat istiyorsan

            return null;
        }
        public async Task<List<PcDto>> GetDistinctPcNamesAsync()
        {
            try
            {
                // DISTINCT PCname sorgusu
                string query = "SELECT DISTINCT PCname FROM PCTrack order by Pcname";
                string url = $"http://10.10.27.108:9000/exec?query={Uri.EscapeDataString(query)}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[QuestDB SELECT] Hata: {response.StatusCode}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var parsed = JsonConvert.DeserializeObject<QuestDbSelectResponse>(json);

                if (parsed?.dataset == null || parsed.dataset.Length == 0)
                    return new List<PcDto>();

                var result = parsed.dataset
                    .Select((row, index) => new PcDto
                    {
                        Id = index + 1,
                        PCname = row[0]
                    })
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[QuestDB SELECT] Exception: {ex.Message}");
                return null;
            }
        }
        public async Task<List<PCTrackDto>> GetAll()
        {
            try
            {
                string query = "SELECT * FROM PCTrack order by Pcname";
                string url = $"http://10.10.27.108:9000/exec?query={Uri.EscapeDataString(query)}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[QuestDB SELECT] Hata: {response.StatusCode}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var parsed = JsonConvert.DeserializeObject<QuestDbSelectResponse>(json);

                var result = new List<PCTrackDto>();

                foreach (var row in parsed.dataset)
                {
                    var dto = new PCTrackDto
                    {
                        PCname = row.Length > 0 ? row[0] : null,
                        ProcessTime = row.Length > 1 ? DateTime.Parse(row[1]) : null,
                        ProcessType = row.Length > 2 ? (ProcessTypes?)int.Parse(row[2]) : null,
                        LoginType = row.Length > 3 ? (LoginType?)int.Parse(row[3]) : null,
                        LoginProcessName = row.Length > 4 ? row[4] : null,
                        LoginId = row.Length > 5 ? row[5] : null,
                        SubjectLoginId = row.Length > 6 ? row[6] : null,
                        ProcessTypeText = row.Length > 2 ? ((ProcessTypes?)int.Parse(row[2])).ToString() : null
                    };

                    result.Add(dto);
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[QuestDB SELECT] Exception: {ex.Message}");
                return null;
            }
        }
    }
}
