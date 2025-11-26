using QuantEngine.Portfolio;
using QuantEngine.Portfolio.Models;
using Xunit;

namespace QuantEngine.Tests.Portfolio;

public class FactorCombinerTests
{
    [Fact]
    public void Combine_ComputesZScores_AndWeightedSum()
    {
        var date = new DateTime(2020, 1, 10);

        var factors = new Dictionary<string, IDictionary<string, decimal>>
        {
            ["Momentum"] = new Dictionary<string, decimal>
            {
                ["A"] = 10m,
                ["B"] = 20m,
                ["C"] = 30m
            }
        };

        var weights = new Dictionary<string, decimal>
        {
            ["Momentum"] = 1m
        };

        var combiner = new FactorCombiner();
        var scores = combiner.Combine(date, factors, weights);

        Assert.Equal(3, scores.Count);

        decimal avg = (10m + 20m + 30m) / 3;
        decimal std = (decimal)Math.Sqrt((double)((10m - avg) * (10m - avg) + (20m - avg) * (20m - avg) + (30m - avg) * (30m - avg)) / 3);

        var expectedZ_A = (10m - avg) / std;

        var scoreA = scores.First(s => s.Ticker == "A").Score;

        Assert.Equal(expectedZ_A, scoreA, precision: 4);
    }
}
