using Services.Enums;

namespace Services.Models
{
    public class Measurement
    {
        public DateTime MeasurementTime { get; set; }
        public double MeasurementValue { get; set; }
        public MeasurementType Type { get; set; }
}
}
