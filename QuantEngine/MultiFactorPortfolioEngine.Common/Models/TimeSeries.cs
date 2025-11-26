namespace QuantEngine.Common.Models;

public sealed class TimeSeries
{
    public string Ticker { get; }
    public IReadOnlyList<PriceBar> Bars { get; }

    public TimeSeries(string ticker, IReadOnlyList<PriceBar> bars)
    {
        Ticker = ticker;
        Bars = bars;
    }
}

