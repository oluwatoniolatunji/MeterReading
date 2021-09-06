using EnergyMeterReading.DataAccess.Contracts;
using EnergyMeterReading.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyMeterReading.DataAccess.Implementation
{
    public class MeterReadingDataAccess : IMeterReadingDataAccess
    {
        private readonly MeterReadingDbContext meterReadingDbContext;

        public MeterReadingDataAccess(MeterReadingDbContext meterReadingDbContext)
        {
            this.meterReadingDbContext = meterReadingDbContext;
        }

        public async Task<List<MeterReading>> GetAsync(int accountId)
        {
            return await meterReadingDbContext.MeterReadings
                .Where(x => x.AccountId == accountId).ToListAsync();
        }

        public async Task SaveAsync(MeterReading meterReading)
        {
            meterReadingDbContext.Add(meterReading);

            await meterReadingDbContext.SaveChangesAsync();
        }

        public async Task SaveAsync(List<MeterReading> meterReadings)
        {
            meterReadingDbContext.AddRange(meterReadings);

            await meterReadingDbContext.SaveChangesAsync();
        }
    }
}
