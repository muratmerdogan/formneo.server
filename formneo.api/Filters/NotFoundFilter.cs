using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using vesa.core.DTOs;
using vesa.core.Models;
using vesa.core.Services;
using vesa.core.Models.TaskManagement;

namespace vesa.api.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {

        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var idValue = context.ActionArguments.Values.FirstOrDefault();

            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (string)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id.ToString() == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) not found"));

        }
    }
}
