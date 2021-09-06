using EnergyMeterReading.Service.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EnergyMeterReading.Api.Middlewares
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var errorObject = new ApiResponseDto
            {
                IsSuccessful = false,
                Error = context.Exception.Message
            };

            context.Result = new BadRequestObjectResult(errorObject);

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            context.ExceptionHandled = true;
        }
    }
}
