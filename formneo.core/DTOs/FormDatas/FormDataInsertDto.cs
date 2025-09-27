using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.FormEnums;

namespace vesa.core.DTOs.FormDatas
{
    public class FormDataInsertDto
    {
        public string? FormName { get; set; }
        public string? FormDescription { get; set; }
        public int Revision { get; set; }
        public string FormDesign { get; set; }

        public int IsActive { get; set; }

        public string JavaScriptCode { get; set; }
        public FormType FormType { get; set; }
        public FormCategory FormCategory { get; set; }
        public FormPriority FormPriority { get; set; }
        public Guid? WorkFlowDefinationId { get; set; }
        public Guid? ParentFormId { get; set; }
        public bool CanEdit { get; set; }
        public bool ShowInMenu { get; set; }


    }
}
