using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace vesa.api.Helper
{
    public static class ValidationHelper
    {
        /// <summary>
        /// ModelState'ten Türkçe validation hatalarını döndürür
        /// </summary>
        /// <param name="modelState">Controller'dan gelen ModelState</param>
        /// <returns>BadRequest result with Turkish error messages</returns>
        public static IActionResult GetValidationErrorResponse(ModelStateDictionary modelState)
        {
            var errors = modelState
                .Where(x => x.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage))
                .ToList();

            return new BadRequestObjectResult(new
            {
                data = (object)null,
                statusCode = 400,
                errors = errors
            });
        }

        /// <summary>
        /// ModelState geçerli mi kontrol eder, değilse Türkçe hata döndürür
        /// </summary>
        /// <param name="modelState">Controller'dan gelen ModelState</param>
        /// <param name="validationResult">Eğer geçersizse hata response'u</param>
        /// <returns>ModelState geçerli mi?</returns>
        public static bool IsValidOrReturnError(ModelStateDictionary modelState, out IActionResult validationResult)
        {
            validationResult = null;

            if (!modelState.IsValid)
            {
                validationResult = GetValidationErrorResponse(modelState);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Custom validation hatası döndürür (tutarlı format)
        /// </summary>
        /// <param name="field">Hata olan alan adı</param>
        /// <param name="message">Hata mesajı</param>
        /// <returns>BadRequest result with consistent format</returns>
        public static IActionResult GetCustomValidationError(string field, string message)
        {
            return new BadRequestObjectResult(new
            {
                data = (object)null,
                statusCode = 400,
                errors = new[] { message }
            });
        }

        /// <summary>
        /// Birden fazla custom validation hatası döndürür
        /// </summary>
        /// <param name="errors">Hata listesi (field, message)</param>
        /// <returns>BadRequest result with consistent format</returns>
        public static IActionResult GetCustomValidationErrors(IEnumerable<(string Field, string Message)> errors)
        {
            return new BadRequestObjectResult(new
            {
                data = (object)null,
                statusCode = 400,
                errors = errors.Select(e => e.Message)
            });
        }
    }
}
