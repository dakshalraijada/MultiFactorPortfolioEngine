using QuantEngine.Portfolio;
using QuantEngine.Portfolio.Models;

namespace QuantEngine.Tests.Portfolio;

public class SimpleLongOnlyBuilderTests
{
    [Fact]
    public void Build_AssignsEqualWeights_ToTopN()
    {
        var scores = new List<FactorScore>
        {
            new("A", new DateTime(2020,1,1), 3),
            new("B", new DateTime(2020,1,1), 2),
            new("C", new DateTime(2020,1,1), 1)
        };

        var builder = new SimpleLongOnlyBuilder();
        var weights = builder.Build(scores, topN: 2);

        Assert.Equal(2, weights.Count);
        Assert.Equal(0.5m, weights["A"]);
        Assert.Equal(0.5m, weights["B"]);
    }

    [Fact]
    public void Build_ReturnsEmpty_WhenNoScores()
    {
        var builder = new SimpleLongOnlyBuilder();
        var weights = builder.Build(Array.Empty<FactorScore>(), topN: 10);

        Assert.Empty(weights);
    }
}
