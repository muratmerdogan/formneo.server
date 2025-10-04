using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.Ticket;

namespace formneo.core.Models.Inventory
{
    public class Inventory : BaseEntity
    {
        // 1. Genel Bilgiler
        public string AssetTag { get; set; } // Envanter No / Varlık Kodu
        public string DeviceName { get; set; }
        public DeviceType? Type { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
        public DeviceStatus? Status { get; set; }
        public string? Description { get; set; }

        // 2. Donanım Bilgileri
        public string? CPU { get; set; }
        public int? RAM { get; set; } // GB
        public DiskType? DiskType { get; set; }
        public int? DiskSize { get; set; } // GB
        public string? GPU { get; set; }
        public string? MACAddress { get; set; }
        public string? StaticIPAddress { get; set; }

        // 3. Yazılım / Lisans Bilgileri
        public string? OperatingSystem { get; set; }
        public LicenseStatus? OS_LicenseStatus { get; set; }
        public string? OfficeLicense { get; set; } // Var / Yok / Versiyon gibi

        // 4. Kullanıcı Bilgileri

        [ForeignKey("UserApp")]
        public string? UserAppId { get; set; }
        public virtual UserApp? UserApp { get; set; }

        [ForeignKey("TicketDepartment")]
        public Guid? TicketDepartmentId { get; set; }
        public virtual TicketDepartment? TicketDepartment { get; set; }
        public OfficeLocation? OfficeLocation { get; set; }

        // 5. Satın Alma / Garanti Bilgileri
        public DateTime? PurchaseDate { get; set; }
        public string? InvoiceOrVendor { get; set; }
        public DateTime? WarrantyEndDate { get; set; }

        // 6. Ek Alanlar
        public string? AssetNumber { get; set; } // Demirbaş No
        //public string? DeliveredBy { get; set; } // Teslim Eden / Alan
        public DateTime? LastMaintenanceDate { get; set; }
        public string? QRorBarcode { get; set; }

    }
    public enum OfficeLocation
    {
        [Description("İstanbul")]
        Istanbul = 0,

        [Description("Eskişehir")]
        Eskisehir = 1,

        [Description("Uşak")]
        Usak = 2,

        [Description("Diğer")]
        Other = 3
    }
    public enum DeviceType
    {
        [Description("Masaüstü")]
        Desktop = 0,

        [Description("Dizüstü")]
        Laptop = 1,

        [Description("Tablet")]
        Tablet = 2,

        [Description("Diğer")]
        Other = 3
    }

    public enum DeviceStatus
    {
        [Description("Aktif")]
        Active = 0,

        [Description("Pasif")]
        Inactive = 1,

        [Description("Tamirde")]
        InRepair = 2,

        [Description("Hurda")]
        Scrapped = 3
    }

    public enum DiskType
    {
        [Description("HDD")]
        HDD = 0,

        [Description("SSD")]
        SSD = 1,

        [Description("NVMe")]
        NVMe = 2
    }

    public enum LicenseStatus
    {
        [Description("Var")]
        Available = 0,

        [Description("Yok")]
        NotAvailable = 1
    }
}