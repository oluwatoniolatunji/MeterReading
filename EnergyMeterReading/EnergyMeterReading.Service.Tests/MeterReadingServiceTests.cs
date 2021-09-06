using EnergyMeterReading.DataAccess.Contracts;
using EnergyMeterReading.DataAccess.Entities;
using EnergyMeterReading.Service.Contracts;
using EnergyMeterReading.Service.Dto;
using EnergyMeterReading.Service.Implementation;
using EnergyMeterReading.Service.Mapping;
using EnergyMeterReading.Service.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyMeterReading.Service.Tests
{
    public class MeterReadingServiceTests
    {
        private Mock<IMeterReadingDataAccess> mockMeterReadingDataAccess;
        private Mock<IMeterReadingMapper> mockMeterReadingMapper;
        private Mock<IAccountService> mockAccountService;

        [SetUp]
        public void SetUp()
        {
            mockMeterReadingDataAccess = new Mock<IMeterReadingDataAccess>();
            mockMeterReadingMapper = new Mock<IMeterReadingMapper>();
            mockAccountService = new Mock<IAccountService>();
        }

        [Test]
        public async Task GetAsync_Returns_Readings_For_Account_That_Exists()
        {
            var accountId = 1092;

            var meterReadings = TestDataHelper.GetTestMeterReadings(accountId);

            var meterReadingDtos = TestDataHelper.GetTestMeterReadingDtos(accountId);

            mockMeterReadingDataAccess.Setup(x => x.GetAsync(accountId)).Returns(Task.FromResult(meterReadings));

            mockMeterReadingMapper.Setup(x => x.Map(meterReadings)).Returns(meterReadingDtos);

            IMeterReadingService meterReadingService = new MeterReadingService(
                mockMeterReadingDataAccess.Object, mockMeterReadingMapper.Object, mockAccountService.Object);

            var response = await meterReadingService.GetAsync(accountId);

            var readingDtos = (List<MeterReadingDto>)response.Data;

            Assert.That(response.IsSuccessful, Is.EqualTo(true));

            Assert.That(readingDtos.Count, Is.EqualTo(meterReadings.Count));
        }

        [Test]
        public async Task GetAsync_Does_Not_Return_Readings_For_Account_That_Does_NOT_Exist()
        {
            var accountId = 1199;

            var meterReadings = new List<MeterReading>();

            var meterReadingDtos = new List<MeterReadingDto>();

            mockMeterReadingDataAccess.Setup(x => x.GetAsync(accountId)).Returns(Task.FromResult(meterReadings));

            mockMeterReadingMapper.Setup(x => x.Map(meterReadings)).Returns(meterReadingDtos);

            IMeterReadingService meterReadingService = new MeterReadingService(
                mockMeterReadingDataAccess.Object, mockMeterReadingMapper.Object, mockAccountService.Object);

            var response = await meterReadingService.GetAsync(accountId);

            var readingDtos = (List<MeterReadingDto>)response.Data;

            Assert.That(response.IsSuccessful, Is.EqualTo(true));

            Assert.That(readingDtos, Is.Empty);
        }

        [Test]
        public async Task ValidateAsync_Returns_A_Valid_Result_If_Reading_Is_Valid()
        {
            var mockMeterReadingDto = TestDataHelper.GetTestMeterReadingDto();

            var mockAccountResponse = TestDataHelper.GetTestAccount(mockMeterReadingDto.AccountId);

            mockMeterReadingDataAccess.Setup(x => x
                .ExistsAsync(mockMeterReadingDto.AccountId, mockMeterReadingDto.MeterReadingDateTime, mockMeterReadingDto.MeterReadValue))
                .Returns(Task.FromResult(false));

            mockAccountService
                .Setup(x => x.GetAccount(mockMeterReadingDto.AccountId))
                .Returns(Task.FromResult(mockAccountResponse));

            IMeterReadingService meterReadingService = new MeterReadingService(
                mockMeterReadingDataAccess.Object, mockMeterReadingMapper.Object, mockAccountService.Object);

            var validationResponse = await meterReadingService.ValidateAsync(mockMeterReadingDto);

            Assert.That(validationResponse.IsValid, Is.True);

            Assert.That(validationResponse.Error, Is.Null);
        }

        [Test]
        public async Task ValidateAsync_Returns_Invalid_Result_If_Reading_Has_Invalid_MeterReadValue()
        {
            var mockMeterReadingDto = TestDataHelper.GetTestMeterReadingDto(meterReadValue: "VOID1");

            var mockAccountResponse = TestDataHelper.GetTestAccount(mockMeterReadingDto.AccountId);

            mockMeterReadingDataAccess.Setup(x => x
                .ExistsAsync(mockMeterReadingDto.AccountId, mockMeterReadingDto.MeterReadingDateTime, mockMeterReadingDto.MeterReadValue))
                .Returns(Task.FromResult(false));

            mockAccountService
                .Setup(x => x.GetAccount(mockMeterReadingDto.AccountId))
                .Returns(Task.FromResult(mockAccountResponse));

            IMeterReadingService meterReadingService = new MeterReadingService(
                mockMeterReadingDataAccess.Object, mockMeterReadingMapper.Object, mockAccountService.Object);

            var validationResponse = await meterReadingService.ValidateAsync(mockMeterReadingDto);

            Assert.That(validationResponse.IsValid, Is.False);

            Assert.That(validationResponse.Error.Contains("Meter reading must digits only"));
        }
    }
}
