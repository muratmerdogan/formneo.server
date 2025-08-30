using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Configuration;
using vesa.core.Models;

namespace vesa.core.DTOs
{


    public  class ApproveItemsDtoResult
    {
        public int Count { get; set; }

        public List<ApproveItemsDto> ApproveItemsDtoList { get; set; }
    }
    public class ApproveItemsDto
    {
        public Guid Id { get; set; }
        public Guid WorkflowItemId { get; set; }


        public string ShortId { get; set; }
        public string ShortWorkflowItemId { get; set; }

        public string ApproveUser { get; set; }
        public string? ApprovedUser_Runtime { get; set; }


        public string ApprovedUser_RuntimeNote { get; set; }
        public string? ApprovedUser_RuntimeNumberManDay { get; set; }

        public string ApproveUserNameSurname { get; set; }
        public string? ApprovedUser_RuntimeNameSurname { get; set; }

        public ApproverStatus ApproverStatus { get; set; }
        public WorkFlowItemDto workFlowItem { get; set; }

        [GmtPlus3]
        public DateTime CreatedDate { get; set; }

        [GmtPlus3]
        public DateTime UpdatedDate { get; set; }


        public int UniqNumber { get; set; }

        public WorkFlowHeadDto WorkFlowHead { get; set; }
    }

    public class ApproveHeadInfo
    {

        public int PendingCount { get; set; }

        public int RejectCount { get; set; }

        public int ApproveCount { get; set; }

        public int SendCount { get; set; }

        public List<ApproveItemsDto> items { get; set; }
    }
}
