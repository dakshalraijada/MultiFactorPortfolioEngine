using QuantEngine.Common.Models;

namespace QuantEngine.Data;

public interface IPriceDataSource
{
    /// <summary>
    /// Returns a time series for a given ticker between the specified dates (inclusive).
    /// </summary>
    Task<TimeSeries> GetHistoryAsync(
        string ticker,
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken = default);
}
