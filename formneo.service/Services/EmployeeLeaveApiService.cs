using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using vesa.core.Models;
using vesa.core.DTOs;
using vesa.core.DTOs.UserCalendar;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace vesa.service.Services
{
    public class EmployeeLeaveApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly string _username;
        private readonly string _password;

        public EmployeeLeaveApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["SapConnectionInfo:apiendpoint"];
            _username = configuration["SapConnectionInfo:username"];
            _password = configuration["SapConnectionInfo:password"];
        }

        public async Task<List<LeaveResponseDto>> GetEmployeeLeaves(LeaveRequestDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var byteArray = Encoding.ASCII.GetBytes($"{_username}:{_password}");
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/zcl_2001_data?sap-client=100", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<LeaveResponseDto>>(responseContent);

                // Adding DayOfWeek in English (lowercase) for each leave
                foreach (var leave in result)
                {
                    if (DateTime.TryParse(leave.Begda, out var leaveDate))
                    {
                        // Set the DayOfWeek to the English name of the day in lowercase
                        leave.DayOfWeek = leaveDate.ToString("dddd", new CultureInfo("en-US")).ToLower();
                    }
                }

                return result;
            }

            return null;
        }

        public async Task<List<HolidayResponseDto>> GetPublicHolidays(DateTime startDate, DateTime endDate)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{_username}:{_password}");
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/zholiday_data?sap-client=100");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<HolidayResponseDto>>(responseContent);

                var filteredResult = result
                                     .Where(h =>
                                     {
                                         if (DateTime.TryParse(h.Tarih, out var holidayDate))
                                         {
                                             return holidayDate >= startDate && holidayDate <= endDate;
                                         }
                                         return false;
                                     })
                                     .GroupBy(h => h.Tarih)
                                     .Select(g => g.First())
                                     .ToList();

                // Adding DayOfWeek in English
                foreach (var holiday in filteredResult)
                {
                    if (DateTime.TryParse(holiday.Tarih, out var holidayDate))
                    {
                        // Set the DayOfWeek to the English name of the day
                        holiday.DayOfWeek = holidayDate.ToString("dddd", new CultureInfo("en-US")).ToLower();
                    }
                }

                return filteredResult;
            }

            return null;
        }

    }
}
