using System.Threading.Tasks;
using formneo.core.DTOs.Onboarding;

namespace formneo.core.Services
{
	public interface IOnboardingService
	{
		Task<OnboardRegisterResponse> RegisterAsync(OnboardRegisterRequest request, string baseUrl);
		Task<OnboardActivateResponse> ActivateAsync(string token);
	}
}
