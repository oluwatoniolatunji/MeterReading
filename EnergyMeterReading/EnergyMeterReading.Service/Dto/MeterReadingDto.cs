using EnergyMeterReading.Service.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EnergyMeterReading.Service.Dto
{
    public class MeterReadingDto : IValidatableObject
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public DateTime MeterReadingDateTime { get; set; }

        [Required]
        [MaxLength(5)]
        [RegularExpression("^[0-9]*$")]
        public string MeterReadValue { get; set; }

        public string UploadErrorMessage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new MeterReadingValidator();

            var result = validator.Validate(this);

            return result.Errors
                .Select(
                    error => new ValidationResult(error.ErrorMessage, new[] { error.PropertyName })
                );
        }
    }
}
