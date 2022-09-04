using Services.Enums;
using Services.Models;

namespace ServicesTests.Data
{
    public static class SampleData
    {
        public static IEnumerable<Measurement> AddMeasurementData()
        {
            return new List<Measurement>()
            {
                new ()
                {
                    MeasurementTime = new DateTime(2022, 01, 03, 10, 04, 45),
                    MeasurementValue = 35.79,
                    Type = MeasurementType.TEMP
                },
                new ()
                {
                    MeasurementTime = new DateTime(2022, 01, 03, 10, 01, 18),
                    MeasurementValue = 98.78,
                    Type = MeasurementType.SPO2
                },
                new()
                {
                    MeasurementTime = new DateTime(2022, 01, 03, 10, 09, 07),
                    MeasurementValue = 35.01,
                    Type = MeasurementType.TEMP
                },
                new ()
                {
                    MeasurementTime = new DateTime(2022, 01, 03, 10, 03, 34),
                    MeasurementValue = 96.49,
                    Type = MeasurementType.SPO2
                },
                new ()
                {
                    MeasurementTime = new DateTime(2022, 01, 03, 10, 02, 01),
                    MeasurementValue = 35.79,
                    Type = MeasurementType.TEMP
                },
                new Measurement()
                {
                    MeasurementTime = new DateTime(2022, 01, 03, 10, 05, 00),
                    MeasurementValue = 97.17,
                    Type = MeasurementType.SPO2
                },
                new Measurement()
                {
                    MeasurementTime = new DateTime(2022, 01, 03, 10, 10, 00),
                    MeasurementValue = 95.08,
                    Type = MeasurementType.SPO2
                }
            };
        }
    }
}
