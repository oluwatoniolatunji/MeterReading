using System;
using System.ComponentModel.DataAnnotations;

namespace EnergyMeterReading.DataAccess.Entities
{
    public class MeterReading
    {
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public DateTime MeterReadingDateTime { get; set; }

        [Required, MaxLength(5)]
        public string MeterReadValue { get; set; }
    }
}
