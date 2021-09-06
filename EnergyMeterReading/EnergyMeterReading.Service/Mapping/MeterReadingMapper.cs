using EnergyMeterReading.DataAccess.Entities;
using EnergyMeterReading.Service.Dto;
using System.Collections.Generic;
using System.Linq;

namespace EnergyMeterReading.Service.Mapping
{
    public class MeterReadingMapper : IMeterReadingMapper
    {
        public List<MeterReadingDto> Map(List<MeterReading> meterReadings)
        => (from meterReading in meterReadings
            select new MeterReadingDto
            {
                AccountId = meterReading.AccountId,
                MeterReadingDateTime = meterReading.MeterReadingDateTime,
                MeterReadValue = meterReading.MeterReadValue
            }).ToList();

        public List<MeterReading> Map(List<MeterReadingDto> meterReadingDtos)
        => (from meterReadingDto in meterReadingDtos
            select new MeterReading
            {
                AccountId = meterReadingDto.AccountId,
                MeterReadingDateTime = meterReadingDto.MeterReadingDateTime,
                MeterReadValue = meterReadingDto.MeterReadValue
            }).ToList();

        public MeterReading Map(MeterReadingDto meterReading)
        => new()
        {
                AccountId = meterReading.AccountId,
                MeterReadingDateTime = meterReading.MeterReadingDateTime,
                MeterReadValue = meterReading.MeterReadValue
            };
    }
}
