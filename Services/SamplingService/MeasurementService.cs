using Services.Models;
using Microsoft.Extensions.Logging;

namespace Services.SamplingService
{
    public class MeasurementService : IMeasurementService
    {
        private readonly ILogger<MeasurementService> _logger;

        public MeasurementService(ILogger<MeasurementService> logger)
        {
            _logger = logger;
        }
        public IEnumerable<Measurement> CalculateSample(IEnumerable<Measurement> samples)
        {
            var sortedSamples = Enumerable.Empty<Measurement>().OrderBy(_ => 1);
            try
            {
                var samplesMeasurements = new List<Measurement>();

                foreach (var sample in samples)
                {
                    var sampleMeasurementDateTime = CheckFiveMinutesIntervalBorder(sample.MeasurementTime);

                    if (sampleMeasurementDateTime.Year == 1900) continue;

                    var measurement = new Measurement()
                    {
                        MeasurementTime = sampleMeasurementDateTime,
                        MeasurementValue = sample.MeasurementValue,
                        Type = sample.Type
                    };
                    samplesMeasurements.Add(measurement);
                }

                if (samplesMeasurements.Any())
                {
                    sortedSamples = samplesMeasurements.OrderBy(x => x.MeasurementTime);
                }

                return sortedSamples;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        private DateTime CheckFiveMinutesIntervalBorder(DateTime measurementTime)
        {
            var minutes = measurementTime.Minute;
            var seconds = measurementTime.Second;

            var newSampleDateTime = measurementTime.AddMinutes(1).AddSeconds(-measurementTime.Second);

            if (minutes % 5 == 0)
            {
                return seconds != 0 ? newSampleDateTime : measurementTime;
            }

            minutes = measurementTime.AddMinutes(1).Minute;
            return (minutes % 5) switch
            {
                0 when seconds != 0 => newSampleDateTime,
                0 => measurementTime,
                _ => new DateTime(1900, 01, 01)
            };
        }
    }
}
