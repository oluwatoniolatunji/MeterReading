using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EnergyMeterReading.DataAccess.Tests.Helpers
{
    public class DbContextHelper
    {
        public static async Task<MeterReadingDbContext> GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<MeterReadingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new MeterReadingDbContext(builder.Options);

            await dbContext.SeedDataAsync();

            return dbContext;
        }
    }
}
