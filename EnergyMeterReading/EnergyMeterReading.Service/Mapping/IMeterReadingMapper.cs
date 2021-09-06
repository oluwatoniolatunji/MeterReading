using EnergyMeterReading.DataAccess.Entities;
using EnergyMeterReading.Service.Dto;
using System.Collections.Generic;

namespace EnergyMeterReading.Service.Mapping
{
    public interface IMeterReadingMapper
    {
        List<MeterReadingDto> Map(List<MeterReading> meterReadings);
        List<MeterReading> Map(List<MeterReadingDto> meterReadingDtos);
        MeterReading Map(MeterReadingDto meterReading);
    }
}
