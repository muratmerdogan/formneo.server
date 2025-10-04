using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using formneo.core.EnumExtensions;

namespace formneo.core.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MainClient
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(120)]
        public string? Slug { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public MainClientStatus Status { get; set; } = MainClientStatus.Pending;

        [Required]
        public MainClientPlan Plan { get; set; } = MainClientPlan.Free;

        [Required]
        [StringLength(64)]
        public string Timezone { get; set; } = "Europe/Istanbul";

        public string? OwnerUserId { get; set; }

        [ForeignKey("OwnerUserId")]
        public UserApp? OwnerUser { get; set; }

        public string? LogoUrl { get; set; }

        // Alan adı yönetimi

        //[StringLength(120)]
        //public string Subdomain { get; set; }

        public string? CustomDomain { get; set; }

        public bool DomainVerified { get; set; } = false;

        // Kota & özellik (JSON string olarak saklanır)
        [Required]
        public string FeatureFlags { get; set; } = "{}";

        [Required]
        public string Quotas { get; set; } = "{}";

        // Faturalama (isteğe bağlı)
        public string? BillingCustomerId { get; set; }
        public string? BillingEmail { get; set; }

        // SSO (opsiyonel)
        public SsoType? SsoType { get; set; }
        public string? SsoMetadataUrl { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsActive { get; set; }

        public MainClient()
        {
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
        }

        // Navigation property for related companies
        public ICollection<Company> Companies { get; set; }
    }

}
