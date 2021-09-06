using EnergyMeterReading.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace EnergyMeterReading.DataAccess.Tests.Helpers
{
    public static class TestDataHelper
    {
        public static async Task SeedDataAsync(this MeterReadingDbContext dbContext)
        {
            var seedData = GetTestAccounts();

            await dbContext.MeterReadings.AddRangeAsync(seedData);

            await dbContext.SaveChangesAsync();
        }

        public static List<MeterReading> GetTestAccounts()
            => new()
            {
                new() { AccountId = 1122, MeterReadingDateTime = DateTime.Now, MeterReadValue="01827" },
                new() { AccountId = 2233, MeterReadingDateTime = DateTime.Now, MeterReadValue = "00000" },
                new() { AccountId = 8766, MeterReadingDateTime = DateTime.Now, MeterReadValue = "00001" },
                new() { AccountId = 2344, MeterReadingDateTime = DateTime.Now, MeterReadValue = "05684" },
                new() { AccountId = 2344, MeterReadingDateTime = DateTime.Now, MeterReadValue = "08234" },
            };
    }
}
