using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Models.BudgetManagement;

namespace formneo.core.DTOs.Budget.NormCodeRequest
{
    public class BudgetNormCodeRequestInsertDto
    {



        public string code { get; set; }
        public DateTime effectiveStartDate { get; set; }
        public DateTime cust_IseBaslamaTarihi { get; set; }
        public DateTime cust_PlanlananIseGiris { get; set; }
        public DateTime cust_plannedEndDate { get; set; }
        public DateTime cust_actualhiredate { get; set; }
        public string? effectiveStatus { get; set; }
        public bool vacant { get; set; }

        //[Required(ErrorMessage = "Değiştirme Nedeni bilgisi gereklidir")]

        public string? changeReason { get; set; }

        //[Required(ErrorMessage = "Pozisyon Şirket Grubu bilgisi gereklidir")]
        public string? cust_GeoZone { get; set; }

        //[Required(ErrorMessage = "Pozisyon Bordro Şirketi bilgisi gereklidir")]
        public string? cust_company { get; set; }

        //[Required(ErrorMessage = "Pozisyon Adı - TR bilgisi gereklidir")]
        public string? externalName_tr_TR { get; set; }

        public string? externalName_defaultValue { get; set; }
        public string? externalName_en_US { get; set; }
        public string? externalName_en_DEBUG { get; set; }
        public string? externalName_ru_RU { get; set; }
        public bool multipleIncumbentsAllowed { get; set; }

        //[Required(ErrorMessage = "Tam Zamanlı bilgisi gereklidir")]
        public string? targetFTE { get; set; }

        // [Required(ErrorMessage = "Haftalık Çalışma Saati bilgisi gereklidir")]
        public string? standardHours { get; set; }

        //[Required(ErrorMessage = "Pozisyon İsmi bilgisi gereklidir")]
        public string jobCode { get; set; }

        // [Required(ErrorMessage = "İş Fonksiyonu bilgisi gereklidir")]
        public string? cust_jobfunction { get; set; }

        // [Required(ErrorMessage = "İş Seviyesi bilgisi gereklidir")]
        public string? cust_ronesansjoblevel { get; set; }

        //[Required(ErrorMessage = "Gelir Seviyesi bilgisi gereklidir")]
        public string? cust_ronesansKademe { get; set; }

        // [Required(ErrorMessage = "Ücret derecesi bilgisi gereklidir")]
        public string? payGrade { get; set; }


        //[Required(ErrorMessage = "İş Ünvanı bilgisi gereklidir")]
        public string? jobTitle { get; set; }

        // [Required(ErrorMessage = "Çalışan Grubu bilgisi gereklidir")]
        public string? employeeClass { get; set; }

        // [Required(ErrorMessage = "Çalışan alt grup bilgisi gereklidir.")]
        public string? cust_empSubGroup { get; set; }

        //[Required(ErrorMessage = "Çalışan tipi bilgisi gereklidir.")]
        public string? cust_EmpGroup { get; set; }

        // [Required(ErrorMessage = "Şirket Grubu bilgisi gereklidir.")]
        public string? cust_companyGroup { get; set; }

        // [Required(ErrorMessage = "Grup/Başkanlık bilgisi gereklidir")]
        public string cust_customlegalEntity { get; set; }

        //[Required(ErrorMessage = "Şirket bilgisi gereklidir")]
        public string businessUnit { get; set; }

        // [Required(ErrorMessage = "Bölge/Fonksiyon/BU bilgisi gereklidir")]
        public string division { get; set; }

        // [Required(ErrorMessage = "Bölüm/Proje/İşletme bilgisi gereklidir")]
        public string cust_sub_division { get; set; }

        // [Required(ErrorMessage = "Birim bilgisi gereklidir")]
        public string? department { get; set; }

        //[Required(ErrorMessage = "1. Üst Birim bilgisi gereklidir")]
        public string? cust_parentDepartment2 { get; set; }

        //[Required(ErrorMessage = "Üst Birim bilgisi gereklidir")]
        public string? cust_parentDepartment { get; set; }

        //[Required(ErrorMessage = "Masraf Merkezi bilgisi gereklidir")]
        public string? costCenter { get; set; }

        // [Required(ErrorMessage = "Personel Alanı bilgisi gereklidir")]
        public string? cust_locationGroup { get; set; }

        //[Required(ErrorMessage = "Personel Alt Alanı bilgisi gereklidir")]
        public string? location { get; set; }

        //[Required(ErrorMessage = "Çalışma Yeri Türü bilgisi gereklidir")]
        public string cust_calismaYeriTuru { get; set; }

        //[Required(ErrorMessage = "Çalışma Yeri Türü bilgisi gereklidir")]
        public string comment { get; set; }

        //[Required(ErrorMessage = "Bodro Alt Birimi bilgisi gereklidir")]
        public string? cust_payGroup { get; set; }

        //[Required(ErrorMessage = "İş Alanı bilgisi gereklidir")]
        public string? cust_isAlani { get; set; }

        //[Required(ErrorMessage = "Fiziksel Lokasyon bilgisi gereklidir")]
        public string? cust_phisicalLocation { get; set; }


        //[Required(ErrorMessage = "Ticket Alacak Mı? bilgisi gereklidir")]
        public string? cust_ticket { get; set; }

        //[Required(ErrorMessage = "Hay Kademe bilgisi gereklidir")]
        public string? cust_HayKademe { get; set; }

        public bool cust_ChiefPosition { get; set; }

        //[Required(ErrorMessage = "Bağlı Olduğu Pozisyon bilgisi gereklidir")]
        public string? parentPosition { get; set; }

        public Boolean IsSend { get; set; }

        public ProcessType ProcessType { get; set; }



        public InternalEmploymentType? InternalEmploymentType { get; set; }


        public bool IsInternalSource { get; set; }
        
        public string relationManager { get; set; }

        public string relationEmployess { get; set; }


        public string Hardware { get; set; }

        public string Licence { get; set; }
        public string internalSourceEmp { get; set; }

        public string? jobCodeDescription { get; set; }
        public bool? isDeleted { get; set; } = false;
        public string? promotionPeriod { get; set; }
        public string? promotionPeriodTxt { get; set; }
        public string? propotionReasonTxt { get; set; }
        public bool? isTransferred { get; set; } = false;
    }
}
