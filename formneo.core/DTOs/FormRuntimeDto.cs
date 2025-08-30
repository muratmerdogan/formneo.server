using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.core.DTOs
{
    public class FormRuntimeDto  : BaseDto
    {

        public Guid Id { get; set; }

        public Guid FormId { get; set; }
        public string ValuesJson { get; set; }

        public string ValuesJsonData { get; set; }
        public bool IsActive { get; set; }
    }
 
    public class FormRuntimeSubmission
    {

        //"_id": "57cf908e6605476b00fab81f",

        public string _id { get; set; }

        [JsonPropertyName("data")]
        public Object ValuesJsonData { get; set; }

    }


}
