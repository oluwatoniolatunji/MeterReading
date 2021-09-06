using EnergyMeterReading.DataAccess.Contracts;
using EnergyMeterReading.DataAccess.Implementation;
using EnergyMeterReading.Service.Contracts;
using EnergyMeterReading.Service.Implementation;
using EnergyMeterReading.Service.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace EnergyMeterReading.Api.Middlewares
{
    public static class ServiceDependencyRegistration
    {
        public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<IMeterReadingDataAccess, MeterReadingDataAccess>();
            services.AddTransient<IMeterReadingService, MeterReadingService>();
            services.AddTransient<IMeterReadingMapper, MeterReadingMapper>();

            return services;
        }
    }
}
