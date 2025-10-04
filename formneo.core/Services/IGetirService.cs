using System.Threading;
using System.Threading.Tasks;
using formneo.core.DTOs.Getir;

namespace formneo.core.Services
{
    public interface IGetirService
    {
        Task HandleNewOrderAsync(string payloadJson);
        Task HandleCancelOrderAsync(string payloadJson);
        Task HandleRestaurantStatusAsync(string payloadJson);
        Task HandleCourierArrivalAsync(string payloadJson);

        Task<GetirPosStatusResponse?> GetPosStatusAsync(CancellationToken cancellationToken = default);
        Task<GetirPosStatusResponse?> SetPosStatusAsync(GetirSetPosStatusRequest request, CancellationToken cancellationToken = default);
        Task<GetirPosStatusResponse?> PostPosStatusAuthAsync(GetirPosStatusPostRequest request, CancellationToken cancellationToken = default);

        Task<GetirAuthLoginResponse?> AuthLoginAsync(GetirAuthLoginRequest request, CancellationToken cancellationToken = default);
        Task<List<GetirPaymentMethodItem>?> GetPaymentMethodsAsync(CancellationToken cancellationToken = default);
    }
}


