using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.workflow
{
    internal class NormCreateRunner : IRunner
    {
        public async void RunAsync(string workflowId)
        {
      
            // POST isteğini yapacağımız URL
            var url = "https://api.cfapps.eu20-001.hana.ondemand.com/api/SFPositions?workFlowId=" + workflowId;

            // HttpClient oluştur
            using (var client = new HttpClient())
            {
                // Boş içerik oluştur
                var content = new StringContent(string.Empty);

                // POST isteğini gönder
                var response = await client.PostAsync(url, content);

                // Yanıtı oku
                var responseString = await response.Content.ReadAsStringAsync();

                // Yanıtı yazdır
                Console.WriteLine(responseString);
            }
        }
    }
}
