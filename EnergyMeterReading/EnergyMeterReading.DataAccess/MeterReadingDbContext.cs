using EnergyMeterReading.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnergyMeterReading.DataAccess
{
   public  class MeterReadingDbContext : DbContext
    {
        public MeterReadingDbContext(DbContextOptions<MeterReadingDbContext> options) : base(options)
        {
        }

        public DbSet<MeterReading> MeterReadings { get; set; }
    }
}
