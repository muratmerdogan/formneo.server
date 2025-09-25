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
                .Select(x => new { 
                    Field = x.Key, 
                    Message = x.Value.Errors.First().ErrorMessage 
                })
                .ToList();

            return new BadRequestObjectResult(new
            {
                message = "Doğrulama hatası",
                errors = errors,
                statusCode = 400
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
    }
}
