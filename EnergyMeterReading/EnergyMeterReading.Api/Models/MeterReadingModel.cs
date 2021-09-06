using CsvHelper.Configuration.Attributes;
using System;

namespace EnergyMeterReading.Api.Models
{
    public class MeterReadingModel
    {
        [Index(0)]
        public string AccountId { get; set; }

        [Index(1)]
        public string MeterReadingDateTime { get; set; }

        [Index(2)]
        public string MeterReadValue { get; set; }
    }
}
