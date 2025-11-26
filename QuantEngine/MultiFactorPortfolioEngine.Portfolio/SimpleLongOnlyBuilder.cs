using QuantEngine.Portfolio.Models;

namespace QuantEngine.Portfolio;

public sealed class SimpleLongOnlyBuilder
{
    public IReadOnlyDictionary<string, decimal> Build(
        IReadOnlyList<FactorScore> scores,
        int topN)
    {
        var selected = scores
            .OrderByDescending(s => s.Score)
            .Take(topN)
            .ToList();

        if (selected.Count == 0)
            return new Dictionary<string, decimal>();

        var weight = 1m / selected.Count;

        return selected
            .GroupBy(s => s.Ticker)
            .ToDictionary(
                g => g.Key,
                _ => weight);
    }
}
