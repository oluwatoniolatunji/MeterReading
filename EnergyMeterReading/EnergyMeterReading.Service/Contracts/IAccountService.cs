using EnergyMeterReading.Service.Dto;
using System.Threading.Tasks;

namespace EnergyMeterReading.Service.Contracts
{
    public interface IAccountService
    {
        Task<ApiResponseDto> GetAccount(int accountId);
    }
}
