using EnergyMeterReading.Service.Contracts;
using EnergyMeterReading.Service.Dto;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Threading.Tasks;

namespace EnergyMeterReading.Service.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration Configuration;

        public AccountService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<ApiResponseDto> GetAccount(int accountId)
        {
            var client = new RestClient(Configuration["AccountService:BaseUrl"]);

            var request = new RestRequest($"/api/Account/GetAccount/{accountId}", Method.GET);

            var response = await client.ExecuteAsync<ApiResponseDto>(request);

            return response.IsSuccessful ? response.Data : null;
        }
    }
}
