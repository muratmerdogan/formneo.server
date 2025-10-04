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
    public class BudgetJobCodeRequest : BaseEntity
    {

        public string JobCode { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Name { get; set; }

    
        public string Name_Ru_RU { get; set; }

        public string Name_En_Debug { get; set; }

        public string Name_Tr_TR { get; set; }

        public string Name_En_US { get; set; }


        public string Description_En_Debug { get; set; }

        public string Description_En_US { get; set; }

        public string Description_Ru_RU { get; set; }

        public string Description_Tr_TR { get; set; }


        public string RequestReason { get; set; }

        public Boolean IsSend { get; set; }

        [ForeignKey("WorkflowHead")]

        public Guid? WorkflowHeadId { get; set; }


        public virtual WorkflowHead? WorkflowHead { get; set; }


        public bool IsFullTime { get; set; }


        public string RegularTemporary { get; set; }

        public string DefaultEmployeeClass { get; set; }

        public bool IsFulltimeEmployee { get; set; }

        public string Grade { get; set; }

        public string JobFunction { get; set; }

        public string PositionLevel { get; set; }

        public string Cust_Joblevelgroup { get; set; }

        public string Cust_Metin { get; set; }

        public string Cust_Jobcode { get; set; }

        public string Cust_AdinesStatus { get; set; }

        public string Cust_EmploymentType { get; set; }

        public string Cust_GorevBirimTipi { get; set; }

        public bool Cust_IsManager { get; set; }

        public string Cust_Bolum { get; set; }

        public string Cust_Ronesanskademe { get; set; }

        public string Cust_Haykademe { get; set; }


    }
}
