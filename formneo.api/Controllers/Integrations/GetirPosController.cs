using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using formneo.core.DTOs.Getir;
using formneo.core.Services;

namespace formneo.api.Controllers.Integrations
{
    [ApiController]
    [Route("integrations/getir/pos-status")]
    public class GetirPosController : ControllerBase
    {
        private readonly IGetirService _getirService;

        public GetirPosController(IGetirService getirService)
        {
            _getirService = getirService;
        }

        [HttpGet]
        public async Task<ActionResult<GetirPosStatusResponse?>> GetPosStatus(CancellationToken cancellationToken)
        {
            var result = await _getirService.GetPosStatusAsync(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GetirPosStatusResponse?>> SetPosStatus([FromBody] GetirSetPosStatusRequest request, CancellationToken cancellationToken)
        {
            var result = await _getirService.SetPosStatusAsync(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("auth")]
        public async Task<ActionResult<GetirPosStatusResponse?>> PostPosStatusAuth([FromBody] GetirPosStatusPostRequest request, CancellationToken cancellationToken)
        {
            var result = await _getirService.PostPosStatusAuthAsync(request, cancellationToken);
            return Ok(result);
        }
    }
}


