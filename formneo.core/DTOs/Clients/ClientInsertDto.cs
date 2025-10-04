using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.EnumExtensions;

namespace formneo.core.DTOs.Clients
{
    public class MainClientInsertDto
    {
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public MainClientStatus Status { get; set; } = MainClientStatus.Pending;
        public MainClientPlan Plan { get; set; } = MainClientPlan.Free;
        public string Timezone { get; set; } = "Europe/Istanbul";
        public string? OwnerUserId { get; set; }
        public string? LogoUrl { get; set; }
        public string Subdomain { get; set; }
        public string? CustomDomain { get; set; }
        public bool DomainVerified { get; set; } = false;
        public string FeatureFlags { get; set; } = "{}";
        public string Quotas { get; set; } = "{}";
        public string? BillingCustomerId { get; set; }
        public string? BillingEmail { get; set; }
        public SsoType? SsoType { get; set; }
        public string? SsoMetadataUrl { get; set; }
    }
}
