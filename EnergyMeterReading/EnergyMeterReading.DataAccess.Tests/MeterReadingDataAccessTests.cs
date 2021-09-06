using EnergyMeterReading.DataAccess.Contracts;
using EnergyMeterReading.DataAccess.Entities;
using EnergyMeterReading.DataAccess.Implementation;
using EnergyMeterReading.DataAccess.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyMeterReading.DataAccess.Tests
{
    public class MeterReadingDataAccessTests
    {
        private MeterReadingDbContext meterReadingDbContext;

        private IMeterReadingDataAccess meterReadingDataAccess;

        [OneTimeSetUp]
        public async Task Setup()
        {
            meterReadingDbContext = await DbContextHelper.GetDbContext();

            meterReadingDataAccess = new MeterReadingDataAccess(meterReadingDbContext);
        }

        [Test]
        public async Task SaveAsync_Saves_MeterReading_Successfully()
        {
            var accountId = 1098;

            var meterReading = new MeterReading() { AccountId = accountId, MeterReadingDateTime = DateTime.Now, MeterReadValue = "01227" };

            await meterReadingDataAccess.SaveAsync(meterReading);

            var meterReadingSaved = await meterReadingDbContext.MeterReadings.FirstOrDefaultAsync(a => a.AccountId == accountId);

            Assert.That(meterReadingSaved.Id, Is.GreaterThan(0));

            Assert.That(meterReadingSaved.AccountId, Is.EqualTo(accountId));
        }

        [Test]
        public async Task SaveAsync_Saves_List_Of_MeterReadings_Successfully()
        {
            var meterReadings = new List<MeterReading>
            {
                new MeterReading() { AccountId = 1222, MeterReadingDateTime = DateTime.Now, MeterReadValue = "11111" },
                new() { AccountId = 2343, MeterReadingDateTime = DateTime.Now, MeterReadValue = "22222" },
                new() { AccountId = 5676, MeterReadingDateTime = DateTime.Now, MeterReadValue = "22345" }
            };

            await meterReadingDataAccess.SaveAsync(meterReadings);

            var accountIds = (from meterReading in meterReadings
                              select meterReading.AccountId).ToList();

            var meterReadingsSaved = await meterReadingDbContext.MeterReadings.CountAsync(a => accountIds.Contains(a.AccountId));

            Assert.That(meterReadingsSaved, Is.EqualTo(3));
        }

        //Account Ids are taken from the data in TestDataHelper
        [TestCase(1122, ExpectedResult = 1)]
        [TestCase(2344, ExpectedResult = 2)]
        [TestCase(0098, ExpectedResult = 0)]
        public async Task<int> GetAsync_Gets_MeterReading_For_Account_If_Account_Exists(int accountId)
        {
            return await meterReadingDbContext.MeterReadings.CountAsync(a => a.AccountId == accountId);
        }

        [Test]
        public async Task ExistsAsync_Is_True_If_MeterReading_Exists()
        {
            var mockMeterReading = await meterReadingDbContext.MeterReadings.FirstOrDefaultAsync();

            var exists = await meterReadingDataAccess
                .ExistsAsync(mockMeterReading.AccountId, mockMeterReading.MeterReadingDateTime, mockMeterReading.MeterReadValue);

            Assert.That(exists, Is.True);
        }

        [Test]
        public async Task ExistsAsync_Is_False_If_MeterReading_Does_NOT_Exist()
        {
            var exists = await meterReadingDataAccess
                .ExistsAsync(1245, DateTime.Now, "09872");

            Assert.That(exists, Is.False);
        }
    }
}