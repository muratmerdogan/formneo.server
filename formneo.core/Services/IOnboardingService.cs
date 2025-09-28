using System.Threading.Tasks;
using vesa.core.DTOs.Onboarding;

namespace vesa.core.Services
{
	public interface IOnboardingService
	{
		Task<OnboardRegisterResponse> RegisterAsync(OnboardRegisterRequest request, string baseUrl);
		Task<OnboardActivateResponse> ActivateAsync(string token);
	}
}
