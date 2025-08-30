using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vesa.core.DTOs;
using vesa.core.DTOs.TicketProjects;
using vesa.core.Models;
using vesa.core.Services;
using vesa.service.Services;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectCategoriesController : CustomBaseController
    {
        private readonly IProjectCategoriesService _projectCategoryService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ProjectCategoriesController(IProjectCategoriesService projectCategoryService, IMapper mapper, IUserService userService)
        {
            _projectCategoryService = projectCategoryService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectCategoriesListDto>>> GetAll()
        {
            try
            {
                var values = await _projectCategoryService.GetAllAsync();
                var orderedValues = values.OrderBy(x => x.Name).ToList();
                return _mapper.Map<List<ProjectCategoriesListDto>>(orderedValues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProjectCategoriesInsertDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Name))
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Kategori adı zorunludur."));
                }
              
                await _projectCategoryService.AddAsync(_mapper.Map<ProjectCategories>(dto));

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }
    }
}
