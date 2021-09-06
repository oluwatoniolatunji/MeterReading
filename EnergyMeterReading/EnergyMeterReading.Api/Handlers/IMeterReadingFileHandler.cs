using EnergyMeterReading.Service.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace EnergyMeterReading.Api.Handlers
{
    public interface IMeterReadingFileHandler
    {
        List<MeterReadingDto> GetMeterReadings(IFormFile formFile);
        bool IsFileValid(IFormFile formFile);
    }
}
