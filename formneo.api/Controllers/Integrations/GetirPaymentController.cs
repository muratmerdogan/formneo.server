using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using formneo.core.DTOs.Getir;
using formneo.core.Services;

namespace formneo.api.Controllers.Integrations
{
    [ApiController]
    [Route("integrations/getir/payment-methods")]
    public class GetirPaymentController : ControllerBase
    {
        private readonly IGetirService _getirService;

        public GetirPaymentController(IGetirService getirService)
        {
            _getirService = getirService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetirPaymentMethodItem>?>> Get(CancellationToken cancellationToken)
        {
            var result = await _getirService.GetPaymentMethodsAsync(cancellationToken);
            return Ok(result);
        }
    }
}


