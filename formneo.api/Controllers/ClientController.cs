using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vesa.core.DTOs.Clients;
using vesa.core.Services;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientController : CustomBaseController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/Client
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var result = await _clientService.GetAllAsync();
            var counts = await _clientService.GetUserCountsByTenantAsync();

            if (result?.Data != null)
            {
                foreach (var item in result.Data)
                {
                    if (counts.TryGetValue(item.Id, out var c))
                        item.UserCount = c;
                    else
                        item.UserCount = 0;
                }
            }

            return CreateActionResult(result);
        }

        // GET: api/Client/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _clientService.GetByIdAsync(id);
            return CreateActionResult(result);
        }

        // POST: api/Client
        [HttpPost]
        public async Task<IActionResult> Save(MainClientInsertDto dto)
        {
            var result = await _clientService.AddAsync(dto);
            return CreateActionResult(result);
        }

        // PUT: api/Client
        [HttpPut]
        public async Task<IActionResult> Update(MainClientUpdateDto dto)
        {
            var result = await _clientService.UpdateAsync(dto);
            return CreateActionResult(result);
        }

        // DELETE: api/Client/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _clientService.RemoveAsync(id);
            return CreateActionResult(result);
        }

        // GET: api/Client/active
        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _clientService.GetActiveClientsAsync();
            return CreateActionResult(result);
        }
    }
}

