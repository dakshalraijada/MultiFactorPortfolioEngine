using QuantEngine.Common.Models;

namespace QuantEngine.Factors;

public interface IFactor
{
    string Name { get; }

    /// <summary>
    /// Computes factor values per date for the given time series.
    /// Returns: Date -> factor value.
    /// </summary>
    IReadOnlyDictionary<DateTime, decimal> Compute(TimeSeries series);
}
