using System;
using System.ComponentModel.DataAnnotations;

namespace vesa.core.DTOs.Onboarding
{
	public class OnboardCompanyDto
	{
		[Required(ErrorMessage = "Şirket adı zorunludur")] public string CompanyName { get; set; }
		[Required(ErrorMessage = "Şirket e-posta zorunludur")] [EmailAddress] public string CompanyEmail { get; set; }
		public string CompanyPhone { get; set; }
		public string CompanyAddress { get; set; }
		public string TaxNumber { get; set; }
		public string Sector { get; set; }
		public string EmployeeCount { get; set; }
	}

	public class OnboardAdminDto
	{
		[Required(ErrorMessage = "Ad zorunludur")] public string FirstName { get; set; }
		[Required(ErrorMessage = "Soyad zorunludur")] public string LastName { get; set; }
		[Required(ErrorMessage = "E-posta zorunludur")] [EmailAddress] public string Email { get; set; }
		public string Phone { get; set; }
		[Required(ErrorMessage = "Şifre zorunludur")] [MinLength(8, ErrorMessage = "Şifre en az 8 karakter olmalıdır")] public string Password { get; set; }
	}

	public class OnboardRegisterRequest
	{
		[Required] public OnboardCompanyDto Company { get; set; }
		[Required] public OnboardAdminDto Admin { get; set; }
		[Required(ErrorMessage = "Plan zorunludur")] public string Plan { get; set; }
		[Range(typeof(bool), "true", "true", ErrorMessage = "Koşullar kabul edilmelidir")] public bool AgreedToTerms { get; set; }
	}

	public class OnboardRegisterResponse
	{
		public string Message { get; set; }
	}

	public class OnboardActivateResponse
	{
		public string Message { get; set; }
	}
}
