using EnergyMeterReading.Service.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyMeterReading.Service.Contracts
{
    public interface IMeterReadingService
    {
        Task<ApiResponseDto> SaveAsync(MeterReadingDto meterReadingDto);
        Task<ApiResponseDto> SaveAsync(List<MeterReadingDto> meterReadingDtos);
        Task<ApiResponseDto> GetAsync(int accountId);
        Task<ValidationDto> ValidateAsync(MeterReadingDto meterReadingDto);
    }
}
