using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using vesa.api.Helper;
using vesa.core.DTOs.Onboarding;
using vesa.core.Services;

namespace vesa.api.Controllers
{
	[Route("api/onboarding")] 
	[ApiController]
	public class OnboardingController : ControllerBase
	{
		private readonly IOnboardingService _service;
		public OnboardingController(IOnboardingService service){_service=service;}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] OnboardRegisterRequest request)
		{
			if (!ValidationHelper.IsValidOrReturnError(ModelState, out var validationResult))
				return validationResult;

			try
			{
				var baseUrl = $"{Request.Scheme}://{Request.Host}";
				var resp = await _service.RegisterAsync(request, baseUrl);
				return Accepted(new { message = resp.Message });
			}
			catch (InvalidOperationException ex)
			{
				// Örn: e-posta zaten mevcut
				return ValidationHelper.GetCustomValidationError("Admin.Email", ex.Message);
			}
		}

		[HttpGet("activate")]
		public async Task<IActionResult> Activate([FromQuery] string token)
		{
			if (string.IsNullOrWhiteSpace(token))
				return ValidationHelper.GetCustomValidationError("token", "Token zorunludur");

			try
			{
				var resp = await _service.ActivateAsync(token);
				return Ok(new { message = resp.Message });
			}
			catch (InvalidOperationException ex)
			{
				// Örn: token geçersiz / süresi dolmuş
				return ValidationHelper.GetCustomValidationError("token", ex.Message);
			}
		}
	}
}
