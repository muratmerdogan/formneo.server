using Newtonsoft.Json;

namespace formneo.api.Config
{

    //test
    public class Config
    {


        public static string SfAddress { get; private set; }
        public static string UserName { get; private set; }
        public static string Password { get; private set; }


        static Config()
        {
            // Bu metot ilk çağrıldığında yani sınıf ilk kullanılmaya başladığında çalışır.
            LoadFromJson("config.json");
        }


        private static void LoadFromJson(string path)
        {
            string jsonContent = System.IO.File.ReadAllText(path);
            var config = JsonConvert.DeserializeObject<ConfigModel>(jsonContent);



            Config.SfAddress = config.SfAddress;
            Config.UserName = config.UserName;
            Config.Password = config.Password;
        }

        public class ConfigModel
        {
            public string SfAddress { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }



}
