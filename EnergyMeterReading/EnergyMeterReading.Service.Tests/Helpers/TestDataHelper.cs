using EnergyMeterReading.DataAccess.Entities;
using EnergyMeterReading.Service.Dto;
using System;
using System.Collections.Generic;

namespace EnergyMeterReading.Service.Tests.Helpers
{
    public class TestDataHelper
    {
        public static List<MeterReadingDto> GetTestMeterReadingDtos()
        => new()
        {
            new() { AccountId = 5654, MeterReadingDateTime = DateTime.Now, MeterReadValue = "12452" },
            new() { AccountId = 2343, MeterReadingDateTime = DateTime.Now, MeterReadValue = "22222" },
            new() { AccountId = 1121, MeterReadingDateTime = DateTime.Now, MeterReadValue = "VOID" },
            new() { AccountId = 1111, MeterReadingDateTime = DateTime.Now, MeterReadValue = "12323" },
            new() { AccountId = 1222, MeterReadingDateTime = DateTime.Now, MeterReadValue = "1232322" }
        };

        public static List<MeterReading> GetTestMeterReadings()
        => new()
        {
            new() { AccountId = 2343, MeterReadingDateTime = DateTime.Now, MeterReadValue = "22222" },
            new() { AccountId = 1111, MeterReadingDateTime = DateTime.Now, MeterReadValue = "12323" }
        };

        public static List<MeterReading> GetTestMeterReadings(int accountId)
        => new()
        {
            new() { AccountId = accountId, MeterReadingDateTime = DateTime.Now, MeterReadValue = "34454" },
            new() { AccountId = accountId, MeterReadingDateTime = DateTime.Now, MeterReadValue = "22234" }
        };

        public static List<MeterReadingDto> GetTestMeterReadingDtos(int accountId)
        => new()
        {
            new() { AccountId = accountId, MeterReadingDateTime = DateTime.Now, MeterReadValue = "34454" },
            new() { AccountId = accountId, MeterReadingDateTime = DateTime.Now, MeterReadValue = "22234" }
        };

        public static MeterReadingDto GetTestMeterReadingDto()
        => new() { AccountId = 5654, MeterReadingDateTime = DateTime.Now, MeterReadValue = "12452" };

        public static MeterReading GetTestMeterReading()
        => new() { AccountId = 5654, MeterReadingDateTime = DateTime.Now, MeterReadValue = "12452" };

        public static MeterReadingDto GetTestMeterReadingDto(string meterReadValue)
        => new() { AccountId = 5654, MeterReadingDateTime = DateTime.Now, MeterReadValue = meterReadValue };

        public static ApiResponseDto GetTestAccount(int accountId)
        {
            if (accountId == 5654 || accountId == 1111)
            {
                return new ApiResponseDto
                {
                    Data = new AccountDto { AccountId = accountId }
                };
            }
            else
            {
                return new ApiResponseDto();
            }
        }
    }
}
