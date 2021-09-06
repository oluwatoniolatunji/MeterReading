using CsvHelper;
using EnergyMeterReading.Api.Models;
using EnergyMeterReading.Service.Contracts;
using EnergyMeterReading.Service.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace EnergyMeterReading.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/meter-reading")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private readonly IMeterReadingService meterReadingService;

        private readonly IMeterReadingFileHandler meterReadingFileHandler;

        public MeterReadingController(IMeterReadingService meterReadingService, IMeterReadingFileHandler meterReadingFileHandler)
        {
            this.meterReadingService = meterReadingService;
            this.meterReadingFileHandler = meterReadingFileHandler;
        }

        [HttpPost("meter-reading-uploads")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseDto))]
        public async Task<IActionResult> MeterReadingUploads(IFormFile formFile)
        {
            var response = new ApiResponseDto();

            var isFileValid = meterReadingFileHandler.IsFileValid(formFile.FileName);

            if (isFileValid)
            {
                var meterReadingDtos = meterReadingFileHandler.GetMeterReadings(formFile);

                if (meterReadingDtos.Any())
                {
                    response = await meterReadingService.SaveAsync(meterReadingDtos);
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Error = "No records found in uploaded file";
                }
            }
            else
            {
                response.IsSuccessful = false;
                response.Error = "Uploaded file must be a csv";
            }

            return Ok(response);
        }

        [HttpPost("readings")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseDto))]
        public async Task<IActionResult> SaveMeterReadingAsync([FromBody] MeterReadingDto meterReadingDto)
        {
            var response = await meterReadingService.SaveAsync(meterReadingDto);

            return Ok(response);
        }

        [HttpGet("accounts/{accountId}/readings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(typeof(IEnumerable<MeterReadingDto>))]
        public async Task<IActionResult> GetMeterReadingForAccountAsync(int accountId)
        {
            var response = await meterReadingService.GetAsync(accountId);

            return Ok(response);
        }
    }
}
