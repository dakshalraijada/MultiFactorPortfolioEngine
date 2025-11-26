using QuantEngine.Common.Models;
using QuantEngine.Factors;
using Xunit;

namespace QuantEngine.Tests.Factors;

public class MomentumFactorTests
{
    private TimeSeries CreateIncreasingSeries()
    {
        var bars = new List<PriceBar>();
        var startDate = new DateTime(2020, 1, 1);

        // 260 days of prices increasing 1 point per day
        for (int i = 0; i < 260; i++)
        {
            var date = startDate.AddDays(i);
            var price = 100m + i;
            bars.Add(new PriceBar("TEST", date, price, price, price, price, 10_000));
        }

        return new TimeSeries("TEST", bars);
    }

    [Fact]
    public void Momentum_IsPositive_WhenPriceIncreases()
    {
        var series = CreateIncreasingSeries();
        var factor = new MomentumFactor(lookbackDays: 252);

        var values = factor.Compute(series);

        var lastDate = series.Bars.Last().Date;

        Assert.True(values.ContainsKey(lastDate));
        Assert.True(values[lastDate] > 0, "Momentum should be positive for rising prices.");
    }

    [Fact]
    public void Momentum_ReturnsEmpty_ForInsufficientHistory()
    {
        var bars = new List<PriceBar>
        {
            new PriceBar("TEST", new DateTime(2020, 1, 1), 100,100,100,100,1000),
            new PriceBar("TEST", new DateTime(2020, 1, 2), 101,101,101,101,1000),
        };

        var series = new TimeSeries("TEST", bars);
        var factor = new MomentumFactor(lookbackDays: 252);

        var values = factor.Compute(series);

        Assert.Empty(values);
    }
}
