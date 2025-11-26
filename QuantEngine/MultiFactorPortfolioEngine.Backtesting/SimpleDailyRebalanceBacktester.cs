using QuantEngine.Common.Models;

namespace QuantEngine.Backtesting;

public sealed class SimpleDailyRebalanceBacktester
{
    public BacktestResult Run(
        IReadOnlyDictionary<DateTime, IReadOnlyDictionary<string, decimal>> dailyWeights,
        IReadOnlyDictionary<string, TimeSeries> seriesByTicker,
        decimal initialCapital = 100_000m)
    {
        var allDates = dailyWeights.Keys.OrderBy(d => d).ToList();
        var equityCurve = new List<(DateTime Date, decimal Equity)>();

        decimal equity = initialCapital;
        decimal peak = equity;
        decimal maxDrawdown = 0m;

        foreach (var date in allDates)
        {
            if (!dailyWeights.TryGetValue(date, out var weightsForDay))
                continue;

            // Compute portfolio one-day return based on close-to-close
            decimal portfolioReturn = 0m;

            foreach (var (ticker, weight) in weightsForDay)
            {
                if (!seriesByTicker.TryGetValue(ticker, out var ts))
                    continue;

                var barTodayIndex = -1;
                for (int i = 0; i < ts.Bars.Count; i++)
                {
                    if (ts.Bars[i].Date == date)
                    {
                        barTodayIndex = i;
                        break;
                    }
                }
                if (barTodayIndex <= 0) continue;

                var prev = ts.Bars[barTodayIndex - 1];
                var curr = ts.Bars[barTodayIndex];

                if (prev.Close <= 0m) continue;
                var r = (curr.Close - prev.Close) / prev.Close;

                portfolioReturn += weight * r;
            }

            equity *= (1m + portfolioReturn);
            peak = Math.Max(peak, equity);
            var dd = (peak - equity) / peak;
            if (dd > maxDrawdown) maxDrawdown = dd;

            equityCurve.Add((date, equity));
        }

        return new BacktestResult
        {
            InitialCapital = initialCapital,
            FinalCapital = equity,
            MaxDrawdown = maxDrawdown,
            EquityCurve = equityCurve
        };
    }
}
