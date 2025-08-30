using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.Models.FormEnums;
using vesa.core.Models.Ticket;

namespace vesa.core.DTOs.FormAssign
{
    public class FormAssignDto
    {
        public Guid Id { get; set; }
        public Guid FormId { get; set; }
        public string? FormName { get; set; }
        public string UserAppId { get; set; }
        public FormStatus Status { get; set; }
        public string? StatusText {  get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? FormRunTimeId { get; set; }

    }
}

