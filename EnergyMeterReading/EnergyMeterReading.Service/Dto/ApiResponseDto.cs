namespace EnergyMeterReading.Service.Dto
{
    public class ApiResponseDto
    {
        public bool IsSuccessful { get; set; } = true;
        public object Data { get; set; }
        public string Error { get; set; }
    }
}
