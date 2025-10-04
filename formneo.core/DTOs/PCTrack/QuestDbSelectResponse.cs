using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.PCTrack
{
    public class QuestDbSelectResponse
    {
        public List<ColumnInfo> columns { get; set; }
        public string[][] dataset { get; set; }
    }

    public class ColumnInfo
    {
        public string name { get; set; }
        public string type { get; set; }
    }
}
