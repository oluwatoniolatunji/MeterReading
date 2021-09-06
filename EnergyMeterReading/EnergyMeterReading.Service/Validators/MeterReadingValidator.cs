using EnergyMeterReading.Service.Dto;
using FluentValidation;
using System;

namespace EnergyMeterReading.Service.Validators
{
    public class MeterReadingValidator : AbstractValidator<MeterReadingDto>
    {
        public MeterReadingValidator()
        {
            RuleFor(x => x.AccountId).NotEqual(0).WithMessage("Account Id is required.");

            RuleFor(x => x.MeterReadingDateTime).Must(BeAValidDate).WithMessage("Meter Reading DateTime is required.");

            RuleFor(x => x.MeterReadValue)
                .NotEmpty().WithMessage("Meter Read Value is required.")
                .MaximumLength(5).WithMessage("Meter Read Value must not exceed 5 characters.")
                .Matches("^[0-9]*$").WithMessage("Meter Read Value must contain only numbers.");
        }

        private static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }
    }
}
