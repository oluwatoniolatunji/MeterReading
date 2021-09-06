using EnergyMeterReading.Service.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace EnergyMeterReading.Api.Controllers
{
    public interface IMeterReadingFileHandler
    {
        List<MeterReadingDto> GetMeterReadings(IFormFile formFile);
        bool IsFileValid(string fileName);
    }
}
