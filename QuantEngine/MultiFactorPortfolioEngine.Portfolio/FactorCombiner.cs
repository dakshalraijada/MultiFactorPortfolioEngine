using QuantEngine.Portfolio.Models;

namespace QuantEngine.Portfolio;

public sealed class FactorCombiner
{
    /// <summary>
    /// Z-score normalises each factor and combines linearly with given weights.
    /// factors: factorName -> (ticker -> value)
    /// factorWeights: factorName -> weight
    /// </summary>
    public IReadOnlyList<FactorScore> Combine(
        DateTime date,
        IDictionary<string, IDictionary<string, decimal>> factors,
        IDictionary<string, decimal> factorWeights)
    {
        var tickers = factors.SelectMany(f => f.Value.Keys).Distinct().ToList();
        var scores = new List<FactorScore>();

        // Precompute z-scores for each factor
        var zScoresPerFactor = new Dictionary<string, Dictionary<string, decimal>>();

        foreach (var (factorName, values) in factors)
        {
            var vals = values.Values.ToList();
            if (!vals.Any()) continue;

            var mean = vals.Average();
            var variance = vals.Select(v => (v - mean) * (v - mean)).DefaultIfEmpty(0m).Average();
            var stdDev = (decimal)Math.Sqrt((double)variance);
            if (stdDev == 0m) stdDev = 1m;

            var z = values.ToDictionary(
                kvp => kvp.Key,
                kvp => (kvp.Value - mean) / stdDev);

            zScoresPerFactor[factorName] = z;
        }

        foreach (var ticker in tickers)
        {
            decimal combined = 0m;

            foreach (var (factorName, weight) in factorWeights)
            {
                if (!zScoresPerFactor.TryGetValue(factorName, out var zMap)) continue;
                if (!zMap.TryGetValue(ticker, out var zVal)) continue;
                combined += weight * zVal;
            }

            scores.Add(new FactorScore(ticker, date, combined));
        }

        return scores;
    }
}
