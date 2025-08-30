using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Services;
using vesa.core.DTOs;
using vesa.core.DTOs.FormAssign;
using vesa.core.DTOs.FormAuth;
using vesa.core.Models;
using vesa.core.Services;
using vesa.service.Services;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormAuthController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<FormAuth, FormAuthDto> _formAuthService;
        private readonly IFormService _formService;
        private readonly IUserService _userService;

        public FormAuthController(IMapper mapper, IServiceWithDto<FormAuth, FormAuthDto> formAuthService, IFormService formService, IUserService userService)
        {
            _mapper = mapper;
            _formAuthService = formAuthService;
            _formService = formService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<FormAuthDto>> GetAll()
        {
            var result = await _formAuthService.Include();
            var data = await result.Include(e => e.Form).Include(e => e.Users).ToListAsync();

            var userServiceResult = await _userService.GetAllUserWithOutPhoto();
            var users = userServiceResult.Data;

            var dtoList = data.Select(item => new FormAuthDto
            {
                Id = item.Id,
                Form = item.Form,
                FormId = (Guid)item.FormId,
                UserIds = item.UserIds,
                Users = users != null && item.UserIds != null
                            ? _mapper.Map<List<UserAppDto>>(
                                users
                                .Where(c => item.UserIds.Contains(Guid.Parse(c.Id)))
                                .ToList()
                              )
                            : null
            }).ToList();

            return dtoList;
        }



        [HttpGet("id")]
        public async Task<FormAuthDto> GetById(Guid id)
        {
            var result = await _formAuthService.Include();

            var data = result.Where(e => e.Id == id).Include(e => e.Form).Include(e => e.Users).FirstOrDefault();

            var users = _userService.GetAllUserWithOutPhoto().Result.Data;

            var dto = new FormAuthDto
            {
                Id = data.Id,
                Form = data.Form,
                FormId = (Guid)data.FormId,
                UserIds = data.UserIds,
                Users = users != null && data.UserIds != null
                            ? _mapper.Map<List<UserAppDto>>(users.Where(c => data.UserIds.Contains(Guid.Parse(c.Id))).ToList())
                            : null
            };
            return dto;
        }

        [HttpPost]
        public async Task<IActionResult> Save(FormAuthInsertDto dto)
        {
            try
            {
                await _formAuthService.AddAsync(_mapper.Map<FormAuthDto>(dto));

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(FormAuthUpdateDto dto)
        {
            try
            {
                var service = await _formAuthService.Include();
                var existing = service.Where(e => e.FormId == dto.FormId).FirstOrDefault();

                if (existing == null)
                {
                    return NotFound("Form auth not found.");
                }

                existing.UserIds = dto.UserIds;

                await _formAuthService.UpdateAsync(_mapper.Map<FormAuthDto>(existing));

                return Ok("Form auth updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the form auth: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var formAuth = await _formAuthService.GetByIdGuidAsync(id);

                if (formAuth == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "formAuth bulunamadı"));
                }

                await _formAuthService.RemoveAsyncByGuid(id);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }



    }
}
