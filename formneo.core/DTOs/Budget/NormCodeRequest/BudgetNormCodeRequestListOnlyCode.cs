using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Models.BudgetManagement;

namespace formneo.core.DTOs.Budget.NormCodeRequest
{


    public class BudgetNormCodeRequestListDtoOnlyCodeResult
    {
        public List<BudgetNormCodeRequestListOnlyCodeDto> BudgetNormCodeRequestListDtoList { get; set; }

        public int Count { get; set; }

    }
    public class BudgetNormCodeRequestListOnlyCodeDto
    {

        public string code { get; set; }
        
        public string? WorkflowHeadId { get; set; }
        public virtual WorkflowHead WorkflowHead { get; set; }


    }
}
