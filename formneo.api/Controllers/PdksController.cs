using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Globalization;
using System.Security.Policy;
using formneo.core.DTOs;
using formneo.core.Models;
using formneo.core.Services;
using WinSCP;
using SessionOptions = WinSCP.SessionOptions;

namespace formneo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdksController : ControllerBase
    {

        public PdksController()
        {

        }

        [HttpGet]
        public async Task<List<HareketDto>> GetAllDepartments(string fileName,string passKey)
        {
            var hareketListesi = new List<HareketDto>();
            try
            {

                if (passKey != "formneo365@&123")
                    return null;
          

                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    HostName = "10.10.27.7",
                    PortNumber = 22,
                    UserName = "sftpuser",
                    Password = "H2xJiY8WyYwHIys",
                    SshHostKeyFingerprint = "ssh-ed25519 255 /rK/pdj1JWPyjTOFdaBj5DrUXj63yD0Yy1Qk+BwzzAI"
                };

                using (Session session = new Session())
                {
                    session.Open(sessionOptions);
                    Console.WriteLine("SFTP bağlantısı başarılı!");

                    string remoteFilePath = "/files/" + fileName;

                    string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string guidFile = Guid.NewGuid().ToString();

                    // Yerel dosya yolunu, çalışma dizininde oluşturun

                    string localFilePath = Path.Combine(applicationDirectory, guidFile + ".xlsx"); ;


                    //string localFilePath = "C:\\Temp\\dosya.xlsx";

                    session.GetFiles(remoteFilePath, localFilePath).Check();

                    using (var package = new ExcelPackage(new FileInfo(localFilePath)))
                    {
                        // İlk sayfayı al
                        var worksheet = package.Workbook.Worksheets[0];

                        // Satırları döngüyle oku (Başlık satırını atla)
                        int rowCount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++) // Satır 2'den başlayacak, 1. satır başlık
                        {
                            var hareket = new HareketDto
                            {
                                SicilNo = worksheet.Cells[row, 1].Text,    // A sütunu
                                Adi = worksheet.Cells[row, 2].Text,         // B sütunu
                                Soyadi = worksheet.Cells[row, 3].Text,      // C sütunu
                                CihazNo = worksheet.Cells[row, 4].Text,     // D sütunu
                                CihazAdi = worksheet.Cells[row, 5].Text,

                                HareketZamani = DateTime.TryParseExact(
                                worksheet.Cells[row, 6].Text,
                                "d.M.yyyy HH:mm:ss",               // Esnek tarih formatı
                                CultureInfo.InvariantCulture,      // Sistemden bağımsız kültür
                                DateTimeStyles.None,               // Zaman dönüşümü stili
                                out var hareketZamani
                                ) ? hareketZamani : DateTime.MinValue,  // Başarısızsa MinValue

                                GirisCikis = worksheet.Cells[row, 7].Text   // G sütunu
                            };

                            hareketListesi.Add(hareket);
                        }
                    }


                    Console.WriteLine("Dosya başarıyla indirildi: " + localFilePath);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }


            return hareketListesi;


        }

    }

    public class HareketDto
    {
        public string SicilNo { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string CihazNo { get; set; }
        public string CihazAdi { get; set; }
        public DateTime HareketZamani { get; set; }
        public string GirisCikis { get; set; }
    }
}
