using EnergyMeterReading.DataAccess.Entities;
using EnergyMeterReading.Service.Dto;
using EnergyMeterReading.Service.Mapping;
using EnergyMeterReading.Service.Tests.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace EnergyMeterReading.Service.Tests
{
    public class MeterReadingMapperTests
    {
        private IMeterReadingMapper meterReadingMapper;

        [SetUp]
        public void SetUp()
        {
            meterReadingMapper = new MeterReadingMapper();
        }

        [Test]
        public void Mapper_Maps_List_Of_Entities_To_List_Of_Dtos_Correctly()
        {
            var meterReadings = TestDataHelper.GetTestMeterReadings();

            var meterReadingDtos = meterReadingMapper.Map(meterReadings);

            Assert.That(meterReadingDtos.Count, Is.EqualTo(meterReadings.Count));

            Assert.That(meterReadingDtos.GetType(), Is.EqualTo(typeof(List<MeterReadingDto>)));
        }

        [Test]
        public void Mapper_Maps_Dtos_To_List_Of_Entities_Correctly()
        {
            var meterReadingDtos = TestDataHelper.GetTestMeterReadingDtos();

            var meterReadings = meterReadingMapper.Map(meterReadingDtos);

            Assert.That(meterReadings.Count, Is.EqualTo(meterReadingDtos.Count));

            Assert.That(meterReadings.GetType(), Is.EqualTo(typeof(List<MeterReading>)));
        }

        [Test]
        public void Mapper_Maps_Dto_To_Entity_Correctly()
        {
            var meterReadingDto = TestDataHelper.GetTestMeterReadingDto();

            var meterReading = meterReadingMapper.Map(meterReadingDto);

            Assert.That(meterReading.AccountId, Is.EqualTo(meterReadingDto.AccountId));

            Assert.That(meterReading.GetType(), Is.EqualTo(typeof(MeterReading)));
        }
    }
}
