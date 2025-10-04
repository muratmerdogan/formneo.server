using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.EnumExtensions;

namespace formneo.core.DTOs.Clients
{
    public class MainClientUpdateDto
    {
        public Guid Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public MainClientStatus Status { get; set; }
        public MainClientPlan Plan { get; set; }
        public string Timezone { get; set; }
        public string? OwnerUserId { get; set; }
        public string? LogoUrl { get; set; }
        public string Subdomain { get; set; }
        public string? CustomDomain { get; set; }
        public bool DomainVerified { get; set; }
        public string FeatureFlags { get; set; }
        public string Quotas { get; set; }
        public string? BillingCustomerId { get; set; }
        public string? BillingEmail { get; set; }
        public SsoType? SsoType { get; set; }
        public string? SsoMetadataUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime UpdatedDate { get; set; }
        public MainClientUpdateDto()
        {
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
