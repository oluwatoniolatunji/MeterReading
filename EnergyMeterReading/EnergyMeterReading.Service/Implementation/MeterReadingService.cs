using EnergyMeterReading.DataAccess.Contracts;
using EnergyMeterReading.Service.Contracts;
using EnergyMeterReading.Service.Dto;
using EnergyMeterReading.Service.Mapping;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EnergyMeterReading.Service.Implementation
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly IMeterReadingDataAccess meterReadingDataAccess;
        private readonly IMeterReadingMapper meterReadingMapper;
        private readonly IAccountService accountService;

        public MeterReadingService(IMeterReadingDataAccess meterReadingDataAccess, IMeterReadingMapper meterReadingMapper, IAccountService accountService)
        {
            this.meterReadingDataAccess = meterReadingDataAccess;
            this.meterReadingMapper = meterReadingMapper;
            this.accountService = accountService;
        }

        public async Task<ApiResponseDto> GetAsync(int accountId)
        {
            var meterReadings = await meterReadingDataAccess.GetAsync(accountId);

            var meterReadingDtos = meterReadingMapper.Map(meterReadings);

            return new ApiResponseDto
            {
                Data = meterReadingDtos
            };
        }

        public async Task<ApiResponseDto> SaveAsync(MeterReadingDto meterReadingDto)
        {
            var validationResult = await ValidateAsync(meterReadingDto);

            if (validationResult.IsValid)
            {
                var meterReading = meterReadingMapper.Map(meterReadingDto);

                await meterReadingDataAccess.SaveAsync(meterReading);

                return new ApiResponseDto();
            }

            return new ApiResponseDto { IsSuccessful = false, Error = validationResult.Error };
        }

        public async Task<ApiResponseDto> SaveAsync(List<MeterReadingDto> meterReadingDtos)
        {
            var uploadResult = new UploadResultDto
            {
                SuccessfulReadings = new List<MeterReadingDto>(),
                FailedReadings = new List<MeterReadingDto>()
            };

            foreach (var meterReadingDto in meterReadingDtos)
            {
                var validationResult = await ValidateAsync(meterReadingDto);

                if (validationResult.IsValid)
                {
                    uploadResult.SuccessfulReadings.Add(meterReadingDto);
                }
                else
                {
                    meterReadingDto.UploadErrorMessage = validationResult.Error;

                    uploadResult.FailedReadings.Add(meterReadingDto);
                }
            }

            var meterReadings = meterReadingMapper.Map(uploadResult.SuccessfulReadings);

            await meterReadingDataAccess.SaveAsync(meterReadings);

            return new ApiResponseDto
            {
                Data = uploadResult
            };
        }

        public async Task<ValidationDto> ValidateAsync(MeterReadingDto meterReadingDto)
        {
            try
            {
                if (string.IsNullOrEmpty(meterReadingDto.MeterReadValue))
                    return new ValidationDto { IsValid = false, Error = "Meter reading is required" };

                if (meterReadingDto.MeterReadValue.Length != 5)
                    return new ValidationDto { IsValid = false, Error = "Meter reading must be of 5 characters" };

                var validMeterReadValue = new Regex(@"^[0-9]*$");

                if (!validMeterReadValue.IsMatch(meterReadingDto.MeterReadValue))
                    return new ValidationDto { IsValid = false, Error = "Meter reading must digits only" };

                var exists = await meterReadingDataAccess
                    .ExistsAsync(meterReadingDto.AccountId, meterReadingDto.MeterReadingDateTime, meterReadingDto.MeterReadValue);

                if (exists)
                    return new ValidationDto { IsValid = false, Error = "Meter reading exists already" };

                var associatedAccountResponse = await accountService.GetAccount(meterReadingDto.AccountId);

                if (associatedAccountResponse.Data == null)
                    return new ValidationDto { IsValid = false, Error = "Meter reading does not have associated account" };

                return new ValidationDto { IsValid = true };
            }
            catch
            {
                return new ValidationDto { IsValid = false, Error = "Error validating meter reading" };
            }
        }
    }
}
