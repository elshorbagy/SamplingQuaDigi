using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Enums;
using Services.SamplingService;
using ServicesTests.Data;

namespace ServicesTests.SamplingService
{
    [TestClass()]
    public class MeasurementServiceTests
    {
        private readonly MeasurementService _measurementService;

        public MeasurementServiceTests()
        {
            var logger = new Mock<ILogger<MeasurementService>>();
            _measurementService = new MeasurementService(logger.Object);
        }

        [TestMethod()]
        public void CalculateSampleTest_Returns_Data()
        {
            var measurements = _measurementService.CalculateSample(SampleData.AddMeasurementData());
            Assert.IsTrue(measurements.Any());
        }

        [TestMethod()]
        public void CalculateSampleTest_Returns_TEMP_Type()
        {
            var measurements = _measurementService.CalculateSample(SampleData.AddMeasurementData());
            Assert.IsNotNull(measurements.FirstOrDefault(x => x.Type == MeasurementType.TEMP));
        }

        [TestMethod()]
        public void CalculateSampleTest_Returns_SPO2_Type()
        {
            var measurements = _measurementService.CalculateSample(SampleData.AddMeasurementData());
            Assert.IsNotNull(measurements.FirstOrDefault(x => x.Type == MeasurementType.SPO2));
        }

        [TestMethod()]
        public void CalculateSampleTest_FiveMinutes_Rule()
        {
            var measurements = _measurementService.CalculateSample(SampleData.AddMeasurementData());

            foreach (var measurement in measurements)
            {
                Assert.IsTrue(measurement.MeasurementTime.Minute % 5 == 0);
                Assert.IsTrue(measurement.MeasurementTime.Second == 0);
            }
        }

        [TestMethod()]
        public void CalculateSampleTest_Output_Sorted_By_DateTime()
        {
            var measurements = _measurementService.CalculateSample(SampleData.AddMeasurementData());
            
            var measurementList = measurements.Where(x=>x.Type == MeasurementType.TEMP).ToList();

            var datetime = measurementList.FirstOrDefault(x=>x.Type == MeasurementType.TEMP)!.MeasurementTime;
            
            for (var i = 1; i < measurementList.Count(); i++)
            {
                Assert.IsTrue(DateTime.Compare(datetime, measurementList[i].MeasurementTime) < 0);
            }

            measurementList = measurements.Where(x => x.Type == MeasurementType.SPO2).ToList();

            datetime = measurementList.FirstOrDefault(x => x.Type == MeasurementType.SPO2)!.MeasurementTime;

            for (var i = 1; i < measurementList.Count(); i++)
            {
                Assert.IsTrue(DateTime.Compare(datetime, measurementList[i].MeasurementTime) < 0);
            }
        }

        [TestMethod()]
        public void CalculateSampleTest_Throw_Exception()
        {
            Assert.ThrowsException<NullReferenceException>(() => _measurementService.CalculateSample(null!));
        }
    }
}