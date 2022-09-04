using Services.Models;

namespace Services.SamplingService
{
    public interface IMeasurementService
    {
        IEnumerable<Measurement> CalculateSample(IEnumerable<Measurement> samples);
    }
}
