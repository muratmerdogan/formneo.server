using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace formneo.core.Models.BudgetManagement
{
    public class BudgetPeriodUser : BaseEntity
    {
        [Key, Column(Order = 0)]
        public string BudgetPeriodCode { get; set; }

        [ForeignKey("BudgetPeriodCode")]
        public BudgetPeriod BudgetPeriod { get; set; }

        [Key, Column(Order = 1)]
        public string UserName { get; set; }

        [Key, Column(Order = 2)]
        public RequestType requestType { get; set; }
        public Permission permission { get; set; }
        public ProcessType processType { get; set; }
        public string? nameSurname { get; set; }
    }


    public enum InternalEmploymentType
    {
        [Description("Terfi/Transfer")]
        TerfiTransfer = 1,

        [Description("Çalışan")]
        Calisan = 2,
    }


    public enum ProcessType
    {
        [Description("Yıllık Kadro Planı")]
        NormKadroTalebi = 1,

        [Description("Ek Kadro Planı")]
        EkKadroTalebi = 2,

    }
    public enum RequestType
    {
        [Description("Norm Kadro Talebi")]
        NormKadroTalebi = 1,

        [Description("Mevcut Kadro")]
        EkKadroTalebi = 2,

        [Description("Terfi Talepleri")]
        TerfiTalepleri = 3,

        [Description("Yeni Pozisyon İsmi Oluştur")]
        YeniPozisyonOluştur = 4,

        [Description("Hepsi")]
        Hepsi = 5,
    }
    public enum Permission  
    {
        [Description("Görüntüleme")]
        view = 1,

        [Description("Yaratma")]
        Create = 2,

    }
}
