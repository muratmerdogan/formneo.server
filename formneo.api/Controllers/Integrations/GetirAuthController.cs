using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using formneo.core.DTOs.Getir;
using formneo.core.Services;

namespace formneo.api.Controllers.Integrations
{
    [ApiController]
    [Route("integrations/getir/auth")] 
    public class GetirAuthController : ControllerBase
    {
        private readonly IGetirService _getirService;

        public GetirAuthController(IGetirService getirService)
        {
            _getirService = getirService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<GetirAuthLoginResponse?>> Login([FromBody] GetirAuthLoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _getirService.AuthLoginAsync(request, cancellationToken);
            return Ok(result);
        }
    }
}


