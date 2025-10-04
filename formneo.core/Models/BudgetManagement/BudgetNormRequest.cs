using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models.BudgetManagement
{
    public class BudgetNormCodeRequest : BaseEntity
    {
        public string? code { get; set; }
        public DateTime? effectiveStartDate { get; set; }
        public DateTime? cust_IseBaslamaTarihi { get; set; }
        public DateTime? cust_PlanlananIseGiris { get; set; }
        public DateTime? cust_plannedEndDate { get; set; }
        public DateTime? cust_actualhiredate { get; set; }
        public string? effectiveStatus { get; set; }
        public bool? vacant { get; set; }
        public string? changeReason { get; set; }
        public string? cust_GeoZone { get; set; }
        public string? cust_company { get; set; }
        public string? externalName_tr_TR { get; set; }
        public string? externalName_defaultValue { get; set; }
        public string? externalName_en_US { get; set; }
        public string? externalName_en_DEBUG { get; set; }
        public string? externalName_ru_RU { get; set; }
        public bool? multipleIncumbentsAllowed { get; set; }
        public string? type { get; set; }
        public string? targetFTE { get; set; }
        public string? standardHours { get; set; }
        public string? jobCode { get; set; }
        public string? cust_jobfunction { get; set; }
        public string? cust_ronesansjoblevel { get; set; }
        public string? cust_ronesansKademe { get; set; }
        public string? payGrade { get; set; }
        public string? jobTitle { get; set; }
        public string? employeeClass { get; set; }
        public string? cust_empSubGroup { get; set; }
        public string? cust_EmpGroup { get; set; }
        public string? cust_companyGroup { get; set; }
        public string? cust_customlegalEntity { get; set; }
        public string? businessUnit { get; set; }
        public string? division { get; set; }
        public string? cust_sub_division { get; set; }
        public string? department { get; set; }
        public string? cust_parentDepartment2 { get; set; }
        public string? cust_parentDepartment { get; set; }
        public string? costCenter { get; set; }
        public string? cust_locationGroup { get; set; }
        public string? location { get; set; }
        public string cust_calismaYeriTuru { get; set; }
        public string? comment { get; set; }
        public string? cust_payGroup { get; set; }
        public string? cust_isAlani { get; set; }
        public string? cust_phisicalLocation { get; set; }
        public string? cust_ticket { get; set; }
        public string? cust_HayKademe { get; set; }
        public bool? cust_ChiefPosition { get; set; }
        public string? parentPosition { get; set; }

        [ForeignKey("WorkflowHead")]
        public Guid? WorkflowHeadId { get; set; }
        public virtual WorkflowHead? WorkflowHead { get; set; }
        public Boolean IsSend { get; set; }

        public ProcessType? ProcessType { get; set; }


        public bool? IsInternalSource { get; set; }
        public InternalEmploymentType? InternalEmploymentType { get; set; }

        public string? relationManager { get; set; }

        public string? relationEmployess { get; set; }

        public string? Hardware { get; set; }

        public string? Licence { get; set; }
        public string? internalSourceEmp { get; set; }
        public string? jobCodeDescription { get; set; }
        public string? promotionPeriod { get; set; }
        public string? promotionPeriodTxt { get; set; }
        public string? propotionReasonTxt { get; set; }
        public bool? isDeleted { get; set; }
        public bool? isTransferred { get; set; }
    }
}
