using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using formneo.core.Services;

namespace formneo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeController : ControllerBase
    {
        private readonly IPermissionEvaluator _evaluator;
        private readonly ITenantContext _tenantContext;

        public MeController(IPermissionEvaluator evaluator, ITenantContext tenantContext)
        {
            _evaluator = evaluator;
            _tenantContext = tenantContext;
        }

        [HttpGet("permissions")]
        public IActionResult GetPermissions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            var tenantId = _tenantContext?.CurrentTenantId;
            var dict = _evaluator.GetAllEffectiveMasks(userId, tenantId);
            return Ok(dict);
        }

        [HttpGet("permissions/{resourceKey}")]
        public IActionResult GetPermissionByResource(string resourceKey, [FromQuery] string action = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            var tenantId = _tenantContext?.CurrentTenantId;

            var mask = _evaluator.GetEffectiveMask(userId, tenantId, resourceKey);
            if (string.IsNullOrWhiteSpace(action))
            {
                return Ok(new { resourceKey, mask });
            }

            // action parametresi varsa bool olarak da dön
            if (Enum.TryParse<formneo.core.Models.Security.Actions>(action, true, out var act))
            {
                var has = _evaluator.Has(userId, tenantId, resourceKey, act);
                return Ok(new { resourceKey, mask, action = act.ToString(), has });
            }
            return BadRequest("invalid action");
        }
    }
}
