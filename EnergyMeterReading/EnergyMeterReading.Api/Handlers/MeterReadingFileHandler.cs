using CsvHelper;
using EnergyMeterReading.Api.Controllers;
using EnergyMeterReading.Api.Models;
using EnergyMeterReading.Service.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace EnergyMeterReading.Api.Handlers
{
    public class MeterReadingFileHandler : IMeterReadingFileHandler
    {
        public List<MeterReadingDto> GetMeterReadings(IFormFile formFile)
        {
            try
            {
                using var stream = formFile.OpenReadStream();

                using var reader = new StreamReader(stream);

                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var uploadedReadings = csv.GetRecords<MeterReadingModel>().ToList();

                var meterReadings = (from reading in uploadedReadings
                                     select new MeterReadingDto
                                     {
                                         AccountId = Convert.ToInt32(reading.AccountId),
                                         MeterReadingDateTime = Convert.ToDateTime(reading.MeterReadingDateTime),
                                         MeterReadValue = reading.MeterReadValue
                                     }).ToList();

                return meterReadings;
            }
            catch
            {
                throw new Exception("Error occurred while processing file. Ensure that file is properly formatte,d has the following headers AccountId, MeterReadingDateTime, MeterReadValue and contains valid date times");
            }
        }

        public bool IsFileValid(string fileName)
            => fileName.EndsWith(".csv");
    }
}
