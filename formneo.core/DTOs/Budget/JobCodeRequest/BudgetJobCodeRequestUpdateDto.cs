using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Models.BudgetManagement;

namespace formneo.core.DTOs.Budget.JobCodeRequest
{
    public class BudgetJobCodeRequestUpdateDto
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "İş Kodu bilgisi gereklidir")]
        public string JobCode { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "İngilizce Adı bilgisi gereklidir")]
        public string Name_En { get; set; }

        [Required(ErrorMessage = "Talep Nedeni bilgisi gereklidir")]
        public string RequestReason { get; set; }

        public Boolean IsSend { get; set; }

        public Guid? WorkflowHeadId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Rusça Adı bilgisi gereklidir")]
        public string Name_Ru_RU { get; set; }

        public string Name_En_Debug { get; set; }

        [Required(ErrorMessage = "Ad bilgisi gereklidir")]
        public string Name_Tr_TR { get; set; }

        [Required(ErrorMessage = "İngilizce Adı bilgisi gereklidir")]
        public string Name_En_US { get; set; }


        public string Description_En_Debug { get; set; }

        public string Description_En_US { get; set; }

        public string Description_Ru_RU { get; set; }

        public string Description_Tr_TR { get; set; }


        public bool IsFullTime { get; set; }


        public string RegularTemporary { get; set; }

        [Required(ErrorMessage = "Varsayılan Çalışan Grubu bilgisi gereklidir")]
        public string DefaultEmployeeClass { get; set; }

        public bool IsFulltimeEmployee { get; set; }

        [Required(ErrorMessage = "Ücret Derecesi bilgisi gereklidir")]
        public string Grade { get; set; }

        [Required(ErrorMessage = "İş İşlevi bilgisi gereklidir")]
        public string JobFunction { get; set; }

        public string PositionLevel { get; set; }

        [Required(ErrorMessage = "JobLevelGroup bilgisi gereklidir")]
        public string Cust_Joblevelgroup { get; set; }

        public string Cust_Metin { get; set; }

        public string Cust_Jobcode { get; set; }

        public string Cust_AdinesStatus { get; set; }

        [Required(ErrorMessage = "İstihdam Tipi bilgisi gereklidir")]
        public string Cust_EmploymentType { get; set; }

        public string Cust_GorevBirimTipi { get; set; }

        public bool Cust_IsManager { get; set; }

        public string Cust_Bolum { get; set; }

        [Required(ErrorMessage = "Gelir Seviyesi- ronesanskademe bilgisi gereklidir")]
        public string Cust_Ronesanskademe { get; set; }

        [Required(ErrorMessage = "Hay Kademe bilgisi gereklidir")]
        public string Cust_Haykademe { get; set; }

    }
}
