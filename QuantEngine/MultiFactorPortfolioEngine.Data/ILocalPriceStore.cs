using QuantEngine.Common.Models;

namespace QuantEngine.Data;

public interface ILocalPriceStore
{
    Task SaveAsync(TimeSeries series, CancellationToken cancellationToken = default);
    Task<TimeSeries?> LoadAsync(string ticker, CancellationToken cancellationToken = default);
}
