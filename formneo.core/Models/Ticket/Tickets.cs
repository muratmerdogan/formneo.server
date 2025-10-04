using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.ComponentModel;

namespace formneo.core.Models.Ticket
{
    public class Tickets : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string TicketCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey("WorkCompany")]
        public Guid WorkCompanyId { get; set; }
        public virtual WorkCompany WorkCompany { get; set; }



        [ForeignKey("TicketDepartment")]
        public Guid? TicketDepartmentId { get; set; }
        public virtual TicketDepartment? TicketDepartment { get; set; }


        [ForeignKey("WorkCompanySystemInfoId")]
        public Guid? WorkCompanySystemInfoId { get; set; }
        public virtual WorkCompanySystemInfo? WorkCompanySystemInfo { get; set; }


        [Required]
        [ForeignKey("UserApp")]
        public string UserAppId { get; set; }
        public virtual UserApp UserApp { get; set; }

        [Required]
        [EnumDataType(typeof(TicketStatus))]
        public TicketStatus Status { get; set; }

        [Required]
        [EnumDataType(typeof(TicketType))]
        public TicketType Type { get; set; } // Enum türünde Ticket Type (ör: Bug, Feature)

        [Required]
        [EnumDataType(typeof(TicketPriority))]
        public TicketPriority Priority { get; set; } // Enum türünde Ticket Priority (ör: High, Medium)

        [Required]
        [EnumDataType(typeof(TicketSubject))]
        public TicketSubject TicketSubject { get; set; } // Enum türünde Ticket Subject(ör: Genel, Medium)


        [Required]
        [EnumDataType(typeof(TicketSLA))]
        public TicketSLA TicketSLA { get; set; }


        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

         public virtual List<TicketComment> TicketComment { get; set; }

        [Required]
        [EnumDataType(typeof(ApproveStatus))]
        public ApproveStatus ApproveStatus { get; set; }


        public Guid? TicketAssigneId { get; set; }
        public virtual TicketAssigne? TicketAssigne { get; set; }


        public Guid? TicketApproveId { get; set; }
        public virtual TicketApprove? TicketApprove { get; set; }

        public bool isTeam { get; set; }

        public bool isApprove { get; set; }

        [ForeignKey("WorkflowHead")]
        public Guid? WorkflowHeadId { get; set; } = null;
        public virtual WorkflowHead? WorkflowHead { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Otomatik artan yapar
        //public int TicketNumber { get; set; }



        //[ForeignKey("TicketCustomer")]
        //public Guid? TicketCustomerId { get; set; } = null;
        //public virtual WorkCompany? TicketCustomer { get; set; } = null;





        [ForeignKey("CustomerRef")]
        public Guid? CustomerRefId { get; set; } = null;
        public virtual WorkCompany? CustomerRef { get; set; } = null;

        public bool IsFromEmail { get; set; }

        public string? MailConversationId { get; set; } = null;
        public string? AddedMailAddresses { get; set; } = null;

        public bool? IsFilePath { get; set; } = false;

        public string? FilePath { get; set; } = null;
        public DateTime? EstimatedDeadline { get; set; }
        [ForeignKey("TicketProjects")]
        public Guid? TicketProjectId { get; set; } = null;
        public virtual TicketProjects? TicketProject{ get; set; } = null;

    }

    public enum ApproveStatus
    {
        [Description("Gönderilmedi")]
        NonSend = 0,

        [Description("Onay Sürecinde")]
        Pending = 1,

        [Description("Reddedildi")]
        Rejected = 2,

        [Description("Onaylandı")]
        Approved = 3
    }


    public enum TicketStatus
    {

        [Description("Taslak")]
        Draft = 1,
        [Description("Açık")]
        Open = 2,                // Açık
        [Description("Atandı")]
        Assigned = 3,            // Atandı
        [Description("Atama Bekliyor")]
        ConsultantWaiting = 4, //Danışman Bekleniyor
        [Description("Devam Ediyor")]
        InProgress = 5,          // Üzerinde Çalışılıyor
        [Description("Birim Testi")]
        InternalTesting = 6,     // İç Test
        [Description("Kullanıcı Kabul Testi")]
        CustomerTesting = 7,     // Müşteri Testi
        [Description("Talep Detay Bekliyor")]
        WaitingForCustomer = 8,  // Müşteriden Yanıt Bekleniyor
        [Description("Tamamlandı")]
        Resolved = 9,            // Çözüldü
        [Description("İptal Edildi")]
        Cancelled = 10,           // İptal Edildi
        [Description("Kapandı")]
        Closed = 11, //Kapandı
        [Description("Onay Sürecinde")]
        InApprove = 12, //Onay Sürecinde
    }

    public enum TicketType
    {
        [Description("BugFix")]
        Bug = 1,        // Hata bildirimi
        [Description("Ek-Talep")]
        Feature = 2,    // Yeni özellik talebi
        //[Description("Görev")]
        //Task = 3,       // Genel bir görev
        //[Description("İyileştirme")]
        //Improvement = 4, // Mevcut bir işlevde iyileştirme talebi
        //[Description("Proje")]
        //Project = 5,
        //[Description("Geliştirme")]
        //Development = 6,
    }

    public enum TicketSubject
    {
        [Description("Genel")]
        General = 1,        // Hata bildirimi
        [Description("Bordro")]
        Bordro = 2,    // Yeni özellik talebi
        [Description("Pozitif Zaman Yönetimi")]
        PozitifZamanYönetimi = 3,       // Genel bir görev
        [Description("Success Factors")]
        SuccessFactors = 4, // Mevcut bir işlevde iyileştirme talebi
        [Description("ABAP")]
        ABAP = 5,
        [Description("Fiori Btp")]
        FioriBtp = 6,
        [Description("IT-HelpDesk")]
        ITHelpDesk = 7,
        [Description("Ücret Modülü")]
        FeeModule = 8,
    }
    public enum TicketSource
    {
        [Description("Telefon")]
        Telefon = 1,        // Hata bildirimi
        [Description("Email")]
        Email = 2,    // Yeni özellik talebi
        [Description("Diğer")]
        Diger = 3,       // Genel bir görev

    }

    public enum TicketSLA
    {
        [Description("Standart")]
        Standart = 1,

        [Description("Premium")]
        Premium = 2,

        //[Description("Sözleşme")]
        //Sözleşme = 3,
    }

    // Ticket Durumları Enum
    public enum TicketPriority
    {
        [Description("Düşük")]
        Low = 1,
        [Description("Orta")]
        Medium = 2,
        [Description("Yüksek")]
        High = 3,
    }
}
