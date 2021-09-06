using EnergyMeterReading.Service.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EnergyMeterReading.Api.Middlewares
{
    public static class GlobalInvalidModelStatedHandler
    {
        public static OkObjectResult GetModelStateError(this ActionContext context)
        {
            var errors = string.Join('\n', context.ModelState.Values.Where(v => v.Errors.Count > 0)
                       .SelectMany(v => v.Errors)
                       .Select(v => v.ErrorMessage)
                   );

            var errorObject = new ApiResponseDto
            {
                IsSuccessful = false,
                Error = errors
            };

            return new OkObjectResult(errorObject);
        }
    }
}
