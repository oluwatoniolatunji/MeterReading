using EnergyMeterReading.Service.Contracts;
using EnergyMeterReading.Service.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace EnergyMeterReading.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private readonly IMeterReadingService meterReadingService;

        public MeterReadingController(IMeterReadingService meterReadingService)
        {
            this.meterReadingService = meterReadingService;
        }

        [HttpPost("SaveMeterReading")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseDto))]
        public async Task<IActionResult> SaveMeterReadingAsync([FromBody] MeterReadingDto meterReadingDto)
        {
            var response = await meterReadingService.SaveAsync(meterReadingDto);

            return Ok(response);
        }

        [HttpGet("GetMeterReadingForAccount/{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(typeof(IEnumerable<MeterReadingDto>))]
        public async Task<IActionResult> GetMeterReadingForAccountAsync(int accountId)
        {
            var response = await meterReadingService.GetAsync(accountId);

            return Ok(response);
        }
    }
}
