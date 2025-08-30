using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs.Budget.SF;

namespace vesa.workflow
{
    public  class Utils
    {

        private readonly UserManager<IdentityUser> _userManager;
        public  string GetNameAndSurnameAsync(string userName)
        {

            UserSFListDto resultObject = new UserSFListDto();
            return userName;
            //if (string.IsNullOrEmpty(userName))
            //{
            //    throw new ArgumentException("User name cannot be null or empty", nameof(userName));
            //}

            //string url = $"https://api.cfapps.eu20-001.hana.ondemand.com/api/SFUser/user/{userName}";

            ////UserSFListDto resultObject = new UserSFListDto();

            //try
            //{
            //    using (HttpClient client = new HttpClient())
            //    {
            //        HttpResponseMessage response = await client.GetAsync(url);
            //        response.EnsureSuccessStatusCode();
            //        string resultString = await response.Content.ReadAsStringAsync();
            //        resultObject = JsonConvert.DeserializeObject<UserSFListDto>(resultString);
            //    }
            //}
            //catch (HttpRequestException e)
            //{
            //    Console.WriteLine("\nException Caught!");
            //    Console.WriteLine("Message :{0} ", e.Message);
            //    throw new ApplicationException($"Error fetching user data for {userName}", e);
            //}

            //return resultObject;
        }


        public static bool ExecuteSql(string sql,string workFlowHeadId)
        {
            // appsettings.json'dan konfigürasyon nesnesini oluştur
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // bulunduğun klasörü al
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Connection string'i al
            string connectionString = configuration.GetConnectionString("SqlServerConnection");


            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();


            SqlCommand cmd = new SqlCommand(sql);
            cmd.CommandText = sql;
            cmd.Parameters.Add("@workFlowHead", System.Data.SqlDbType.UniqueIdentifier).Value = workFlowHeadId;
            cmd.Connection = con;



            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adp.Fill(dt);


            if (dt.Rows.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static string ShortenGuid(Guid guid, int size = 10)
        {
            // GUID'i byte dizisine dönüştür
            byte[] guidBytes = guid.ToByteArray();

            // Byte dizisini Base64 string'e dönüştür
            string base64 = Convert.ToBase64String(guidBytes);

            // Base64 string'deki '/' ve '+' karakterlerini URL ve dosya adı için güvenli olan '_' ve '-' karakterleriyle değiştir
            base64 = base64.Replace("/", "_").Replace("+", "-").TrimEnd('=');

            // Base64 string'den istediğimiz uzunluğu almak için substring kullan
            if (base64.Length > size)
            {
                base64 = base64.Substring(0, size);
            }
            else
            {
                // Eğer base64 string yeterince uzun değilse, eksik kalan karakterleri tamamlamak için tekrar başa dön
                while (base64.Length < size)
                {
                    base64 += base64.Substring(0, size - base64.Length);
                }
            }

            return base64;
        }

    }
}
