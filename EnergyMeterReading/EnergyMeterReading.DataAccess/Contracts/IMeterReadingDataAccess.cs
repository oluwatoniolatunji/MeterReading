using EnergyMeterReading.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyMeterReading.DataAccess.Contracts
{
    public interface IMeterReadingDataAccess
    {
        Task SaveAsync(MeterReading meterReading);
        Task SaveAsync(List<MeterReading> meterReadings);
        Task<List<MeterReading>> GetAsync(int accountId);
    }
}
