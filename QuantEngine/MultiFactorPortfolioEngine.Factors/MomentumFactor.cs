using QuantEngine.Common.Models;

namespace QuantEngine.Factors;

public sealed class MomentumFactor : IFactor
{
    public string Name => "Momentum";
    private readonly int _lookbackDays;

    public MomentumFactor(int lookbackDays = 252)
    {
        _lookbackDays = lookbackDays;
    }

    public IReadOnlyDictionary<DateTime, decimal> Compute(TimeSeries series)
    {
        var bars = series.Bars.OrderBy(b => b.Date).ToList();
        var result = new Dictionary<DateTime, decimal>();

        // Simple price-based momentum: Close(t) / Close(t - lookback) - 1
        for (int i = 0; i < bars.Count; i++)
        {
            var current = bars[i];
            var lookbackBar = bars.FirstOrDefault(b =>
                (current.Date - b.Date).TotalDays >= _lookbackDays - 5 && // small tolerance
                (current.Date - b.Date).TotalDays <= _lookbackDays + 5);

            if (lookbackBar is null)
                continue;

            if (lookbackBar.Close <= 0m) continue;

            var mom = current.Close / lookbackBar.Close - 1m;
            result[current.Date] = mom;
        }

        return result;
    }
}
