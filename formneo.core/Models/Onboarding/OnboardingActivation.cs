using System;
using System.ComponentModel.DataAnnotations;
using vesa.core.Models;

namespace vesa.core.Models.Onboarding
{
	public class OnboardingActivation : GlobalBaseEntity
	{
		[Required]
		public string Token { get; set; }
		[Required]
		public string Plan { get; set; }
		[Required]
		public bool AgreedToTerms { get; set; }

		// Company
		[Required]
		public string CompanyName { get; set; }
		[Required]
		public string CompanyEmail { get; set; }
		public string CompanyPhone { get; set; }
		public string CompanyAddress { get; set; }
		public string TaxNumber { get; set; }
		public string Sector { get; set; }
		public string EmployeeCount { get; set; }

		// Admin
		[Required]
		public string AdminFirstName { get; set; }
		[Required]
		public string AdminLastName { get; set; }
		[Required]
		public string AdminEmail { get; set; }
		public string AdminPhone { get; set; }
		[Required]
		// AdminPasswordHash kaldırıldı; aktivasyonda geçici parola üretilecek

		public DateTime ExpiresAt { get; set; }
		public DateTime? ActivatedAt { get; set; }
		public bool IsUsed { get; set; }
	}
}
