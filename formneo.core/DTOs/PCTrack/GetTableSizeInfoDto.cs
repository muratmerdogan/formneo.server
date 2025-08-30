using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.PCTrack
{
    public class GetTableSizeInfoDto
    {
        public string TableName { get; set; }
        public string SchemaName { get; set; }
        public long RowCounts { get; set; }
        public decimal TotalSizeMB { get; set; }
        public decimal UsedSizeMB { get; set; }
        public decimal DataSizeMB { get; set; }
    }
}
