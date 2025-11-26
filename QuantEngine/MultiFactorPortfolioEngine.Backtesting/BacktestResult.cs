namespace QuantEngine.Backtesting;

public sealed class BacktestResult
{
    public decimal InitialCapital { get; init; }
    public decimal FinalCapital { get; init; }
    public decimal MaxDrawdown { get; init; }
    public IReadOnlyList<(DateTime Date, decimal Equity)> EquityCurve { get; init; } = [];
}
