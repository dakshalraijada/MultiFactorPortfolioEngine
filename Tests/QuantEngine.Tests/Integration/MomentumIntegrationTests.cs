using QuantEngine.Common.Models;
using QuantEngine.Factors;
using QuantEngine.Portfolio;
using QuantEngine.Portfolio.Models;
using QuantEngine.Backtesting;
using Xunit;

namespace QuantEngine.Tests.Integration;

public class MomentumIntegrationTests
{
    [Fact]
    public void Momentum_Strategy_ProducesValidEquityCurve()
    {
        // Create artificial price data for two stocks
        var tsA = new TimeSeries("A", Enumerable.Range(0, 260)
            .Select(i =>
            {
                var price = 100m + i; // trending up
                var date = new DateTime(2020, 1, 1).AddDays(i);
                return new PriceBar("A", date, price, price, price, price, 1000);
            }).ToList());

        var tsB = new TimeSeries("B", Enumerable.Range(0, 260)
            .Select(i =>
            {
                var price = 200m - i; // trending down
                var date = new DateTime(2020, 1, 1).AddDays(i);
                return new PriceBar("B", date, price, price, price, price, 1000);
            }).ToList());

        var dict = new Dictionary<string, TimeSeries>
        {
            ["A"] = tsA,
            ["B"] = tsB
        };

        // 1. Compute factor
        var factor = new MomentumFactor(252);

        var factorsByTicker = new Dictionary<string, decimal>();
        var momentumA = factor.Compute(tsA);
        var momentumB = factor.Compute(tsB);

        var date = tsA.Bars.Last().Date;

        factorsByTicker["A"] = momentumA[date];
        factorsByTicker["B"] = momentumB[date];

        // 2. Combine (single factor)
        var combiner = new FactorCombiner();

        var combined = combiner.Combine(
            date,
            new Dictionary<string, IDictionary<string, decimal>>
            {
                ["Momentum"] = factorsByTicker
            },
            new Dictionary<string, decimal> { ["Momentum"] = 1m });

        // 3. Build portfolio (pick top 1)
        var builder = new SimpleLongOnlyBuilder();
        var weights = builder.Build(combined, topN: 1);

        Assert.Single(weights);
        Assert.True(weights.ContainsKey("A"), "A has better momentum and should be selected.");

        // 4. Backtest
        var weightsByDate = new Dictionary<DateTime, IReadOnlyDictionary<string, decimal>>
        {
            [date] = weights
        };

        var backtester = new SimpleDailyRebalanceBacktester();
        var result = backtester.Run(weightsByDate, dict);

        Assert.True(result.FinalCapital > result.InitialCapital);
        Assert.NotEmpty(result.EquityCurve);
    }
}
