using System.Collections.Generic;

namespace EnergyMeterReading.Service.Dto
{
    public class UploadResultDto
    {
        public List<MeterReadingDto> SuccessfulReadings { get; set; }
        public List<MeterReadingDto> FailedReadings { get; set; }
    }
}
