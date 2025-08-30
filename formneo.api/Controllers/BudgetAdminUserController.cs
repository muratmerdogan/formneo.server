using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using System.Dynamic;
using vesa.core.DTOs;
using vesa.core.DTOs.Budget.BudgetAdminUser;
using vesa.core.DTOs.Budget.PeriodUserDto;
using vesa.core.DTOs.Clients;
using vesa.core.EnumExtensions;
using vesa.core.Models;
using vesa.core.Models.BudgetManagement;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vesa.api.Controllers
{
    //[Authorize]

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BudgetAdminUserController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<BudgetAdminUser, BudgetAdminUserListDto> _service;

        public BudgetAdminUserController(IMapper mapper, IServiceWithDto<BudgetAdminUser, BudgetAdminUserListDto> service)
        {
            _mapper = mapper;

            _service = service;


        }
        /// GET api/products
        [HttpGet]
        public async Task<List<BudgetAdminUserListDto>> All()
        {
            var forms = await _service.GetAllAsync();




            var processedData = forms.Data
                    .Select(d => new BudgetAdminUserListDto
                    {
                        Id = d.Id,
                        AdminLevel = d.AdminLevel,
                        adminLevelText = d.AdminLevel.GetDescription(),
                        CreatedBy = d.CreatedBy,
                        UserName = d.UserName,
                        CreatedDate = d.CreatedDate,
                        IsAdmin = d.IsAdmin,
                        IsDoProxy = d.IsDoProxy,
                        Mail = d.Mail,
                        ProxyUser = d.ProxyUser,
                        UpdatedBy = d.UpdatedBy,
                        UpdatedDate = d.UpdatedDate
                    })
                    .ToList().OrderBy(e => e.CreatedDate);

            return processedData.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetAdminUserListDto>> GetById(string id)
        {
            var data = await _service.GetByIdGuidAsync(new Guid(id));


            data.Data.adminLevelText = data.Data.AdminLevel.GetDescription();
            return data.Data;
        }
        // GET /api/products/5

        //de
        [HttpPost]
        public async Task<ActionResult<BudgetAdminUserListDto>> Save(BudgetAdminUserInsertDto dto)
        {

          

            var result = await _service.AddAsync(_mapper.Map<BudgetAdminUserListDto>(dto));

            return result.Data;

        }
        [HttpPut]
        public async Task<ActionResult<BudgetAdminUserUpdateDto>> Update(BudgetAdminUserUpdateDto dto)
        {

            await _service.UpdateAsync(_mapper.Map<BudgetAdminUserListDto>(dto));

            return dto;
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {

            await _service.RemoveAsyncByGuid(new Guid(id));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }


        [HttpGet("GetUsernName/{userName}")]
        public async Task<ActionResult<BudgetAdminUserListDto>> GetUserName(string userName)
        {
            var data = await _service.GetAllAsync();
            var item = data.Data.Where(e => e.UserName == userName).FirstOrDefault();
            //item.adminLevelText = item.AdminLevel.GetDescription();
            return item;
        }

        //[HttpGet("[action]/{formId}")]
        //public async Task<FormRuntimeDto> getFormDataById(Guid formId)
        //{
        //    var forms = await _service.Find(e => e.FormId == formId);

        //    var dto = _mapper.Map<FormRuntimeDto>(forms.Data);

        //    return dto;
        //}


        [HttpGet("CheckProxyUser/{userid},{proxyUser}")]
        public async Task<bool> CheckProxyUser(string userid, string proxyUser)
        {
            var data = await _service.GetAllAsync();
            var result = data.Data.Where(e => e.UserName == userid).FirstOrDefault();

            if (result == null)
                return false;

            if (result.IsDoProxy)
            {
                if (!string.IsNullOrEmpty(result.ProxyUser))
                {
                    var array = result.ProxyUser.Split(',');
                    bool exists = array.Contains(proxyUser);
                    return exists;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }


}
