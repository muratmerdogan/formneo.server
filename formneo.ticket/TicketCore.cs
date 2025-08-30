using MySqlConnector;
using System.Data;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using System.Reflection.Emit;

namespace vesa.ticket
{
    public class TicketCore
    {
        public List<ExcelList> GetTaskList()
        {

            using var connection = new MySqlConnection("Server=104.247.163.226;User ID=mmerdogan;Password=ExpoExpo2023++;Database=support");

            connection.Open();


            using var command = new MySqlCommand("SELECT * FROM ost_task INNER JOIN ost_task__cdata ON ost_task__cdata.task_id = ost_task.id\r\n\r\nINNER JOIN  ost_staff ON ost_staff.staff_id = ost_task.staff_id\r\n\r\nLEFT JOIN ost_ticket ON ost_ticket.ticket_id = ost_task.object_id\r\n\r\nLEFT JOIN ost_ticket__cdata ON ost_ticket__cdata.ticket_id = ost_task.object_id\r\n\r\n\r\nwhere ost_task.flags=1\r\n\r\n", connection);


            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            List<ExcelList> list= new List<ExcelList>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {

                    if(i==100)
                    {


                    }
                    ExcelList item = new ExcelList();
                    item.ID = dt.Rows[i]["id"].ToString();
                    item.Project = dt.Rows[i]["subject"].ToString();
                    item.Consultant = dt.Rows[i]["firstname"].ToString() + " " + dt.Rows[i]["lastname"].ToString();
                    item.Customer = dt.Rows[i]["comp"].ToString();
                    item.Task = dt.Rows[i]["title"].ToString();
                    item.Team = dt.Rows[i]["dept_id"].ToString();

                    item.completion = dt.Rows[i]["complated"].ToString();

                    item.note = Regex.Replace(dt.Rows[i]["projectdescription"].ToString(), "<.*?>", "");

                    string[] parts = System.Text.RegularExpressions.Regex.Unescape(dt.Rows[i]["stat"].ToString()).Split(',');

                    if (parts.Length > 0)
                    {
                        if (parts[0] != "")
                        {
                            item.Status = GetStatusText(parts[0]);
                        }
                    }
                    //item.CreateDate = Convert.ToDateTime(dt.Rows[i]["created"].ToString());

                    item.Duration = (DateTime.Now - Convert.ToDateTime(dt.Rows[i]["created"].ToString())).Days;
                    list.Add(item);

                    list = list.OrderByDescending(e => e.Consultant).ToList();
                }
                catch
                {


                }
            }

             
            //list = list.Where(e => e.Team == "25" || e.Team == "27").ToList();
            return list;
        
        }

        public List<AllList> GetSelectList()
        {

            using var connection = new MySqlConnection("Server=104.247.163.226;User ID=mmerdogan;Password=ExpoExpo2023++;Database=support");

            connection.Open();


            using var command = new MySqlCommand("SELECT * FROM ost_list_items", connection);



            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            List<AllList> list = new List<AllList>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AllList item = new AllList();


                item.id = Convert.ToInt32(dt.Rows[i]["id"]?.ToString() ?? "0");

                if (dt.Rows[i]["list_id"] == DBNull.Value)
                {
                    // list_id NULL ise yapılacak işlem
                    // Örneğin, item.list_id'yi bir varsayılan değere ayarlayabilirsiniz veya bir hata mesajı gösterebilirsiniz.
                    item.list_id = -1;  // Varsayılan değer olarak -1 kullanıldı.
                }
                else
                {
                    item.list_id = Convert.ToInt32(dt.Rows[i]["list_id"].ToString());
                }

                item.value = dt.Rows[i]["value"]?.ToString() ?? string.Empty;
                list.Add(item);

       
            }
            return  list;

        }
        public List<ExcelList> GetTaskListAll()
        {

            using var connection = new MySqlConnection("Server=104.247.163.226;User ID=mmerdogan;Password=ExpoExpo2023++;Database=support");

            connection.Open();


            using var command = new MySqlCommand("SELECT * FROM ost_task INNER JOIN ost_task__cdata ON ost_task__cdata.task_id = ost_task.id\r\n\r\nINNER JOIN  ost_staff ON ost_staff.staff_id = ost_task.staff_id\r\n\r\nLEFT JOIN ost_ticket ON ost_ticket.ticket_id = ost_task.object_id\r\n\r\nLEFT JOIN ost_ticket__cdata ON ost_ticket__cdata.ticket_id = ost_task.object_id\r\n\r\n\r\nwhere ost_task.flags=1\r\n\r\n", connection);


            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            List<ExcelList> list = new List<ExcelList>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                try
                {

                    if (i == 108)
                    {

                    }

                    ExcelList item = new ExcelList();
                    item.ID = dt.Rows[i]["id"].ToString();
                    item.Project = dt.Rows[i]["subject"].ToString();
                    item.Consultant = dt.Rows[i]["firstname"].ToString() + " " + dt.Rows[i]["lastname"].ToString();
                    item.Customer = dt.Rows[i]["comp"].ToString();
                    item.Task = dt.Rows[i]["title"].ToString();
                    item.Team = dt.Rows[i]["dept_id"].ToString();

                    item.completion = dt.Rows[i]["complated"].ToString();

                    item.note = Regex.Replace(dt.Rows[i]["projectdescription"].ToString(), "<.*?>", "");

                    string[] parts = System.Text.RegularExpressions.Regex.Unescape(dt.Rows[i]["stat"].ToString()).Split(',');

                    if (parts.Length > 0)
                        if (parts[0] != "")
                        {
                            item.Status = GetStatusText(parts[0]);
                        }


                    //item.CreateDate = Convert.ToDateTime(dt.Rows[i]["created"].ToString());

                    item.Duration = (DateTime.Now - Convert.ToDateTime(dt.Rows[i]["created"].ToString())).Days;
                    list.Add(item);

                    list = list.OrderByDescending(e => e.Consultant).ToList();

                }
                catch (Exception ex)
                {


                }

            }


            return list;

        }
        private static string GetStatusText(string number)
        {
            switch (Convert.ToInt32(number))
            {
                case 1:
                    return "Başlanmadı";
                case 2:
                    return "Geliştirme Aşamasında";
                case 3:
                    return "Danışman Bekliyor";
                case 5:
                    return "Müşteri Testi";
                case 6:
                    return "Tamamlandı";
                default:
                    return "Bilinmeyen Durum";
            }
        }
    }

    public class AllList
    {

        public int id { get; set; }
        public int list_id { get; set; }
        public int status { get; set; }
        public string value { get; set; }

    }
    public class ExcelList
    {


        public string ID { get; set; }

        public string Team { get; set; }
        public string Project { get; set; }

        public string Consultant { get; set; }
        public string Customer { get; set; }

        public string Task { get; set; }

        public string Status { get; set; }

        public string completion { get; set; }
        //public DateTime CreateDate { get; set; }


        public string note { get; set; }
        public int  Duration { get; set; }


    }

}