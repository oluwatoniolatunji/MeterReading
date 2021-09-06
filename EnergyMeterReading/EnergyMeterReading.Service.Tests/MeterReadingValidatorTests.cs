using EnergyMeterReading.Service.Dto;
using EnergyMeterReading.Service.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System;

namespace EnergyMeterReading.Service.Tests
{
    public class MeterReadingValidatorTests
    {
        private MeterReadingValidator meterReadingValidator;

        [SetUp]
        public void Setup()
        {
            meterReadingValidator = new MeterReadingValidator();
        }

        [Test]
        public void Should_Not_Have_Any_Errors()
        {
            var model = new MeterReadingDto { AccountId = 2343, MeterReadingDateTime = DateTime.Now, MeterReadValue = "22222" };

            var result = meterReadingValidator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Should_Have_Error_When_Account_Id_Is_Not_Provided()
        {
            var model = new MeterReadingDto { MeterReadingDateTime = DateTime.Now, MeterReadValue = "22222" };

            var result = meterReadingValidator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(a => a.AccountId)
                .WithErrorMessage("Account Id is required.");
        }

        [Test]
        public void Should_Have_Error_When_MeterReadingDateTime_Is_Not_Provided()
        {
            var model = new MeterReadingDto { AccountId = 2343, MeterReadValue = "22222" };

            var result = meterReadingValidator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(a => a.MeterReadingDateTime)
                .WithErrorMessage("Meter Reading DateTime is required.");
        }

        [Test]
        public void Should_Have_Error_When_MeterReadValue_Is_Not_Provided()
        {
            var model = new MeterReadingDto { AccountId = 2343, MeterReadingDateTime = DateTime.Now };

            var result = meterReadingValidator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(a => a.MeterReadValue)
                .WithErrorMessage("Meter Read Value is required.");
        }

        [Test]
        public void Should_Have_Error_When_MeterReadValue_Is_More_Than_5_Digits()
        {
            var model = new MeterReadingDto { AccountId = 2343, MeterReadingDateTime = DateTime.Now, MeterReadValue = "122222" };

            var result = meterReadingValidator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(a => a.MeterReadValue)
                .WithErrorMessage("Meter Read Value must not exceed 5 characters.");
        }

        [Test]
        public void Should_Have_Error_When_MeterReadValue_Contains_Non_Digit_Characters()
        {
            var model = new MeterReadingDto { AccountId = 2343, MeterReadingDateTime = DateTime.Now, MeterReadValue = "0X765" };

            var result = meterReadingValidator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(a => a.MeterReadValue)
                .WithErrorMessage("Meter Read Value must contain only numbers.");
        }
    }
}