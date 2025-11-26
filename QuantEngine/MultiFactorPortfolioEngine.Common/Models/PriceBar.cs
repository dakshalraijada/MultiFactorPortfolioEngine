namespace QuantEngine.Common.Models;

public sealed class PriceBar
{
    public string Ticker { get; }
    public DateTime Date { get; }
    public decimal Open { get; }
    public decimal High { get; }
    public decimal Low { get; }
    public decimal Close { get; }
    public long Volume { get; }

    public PriceBar(
        string ticker,
        DateTime date,
        decimal open,
        decimal high,
        decimal low,
        decimal close,
        long volume)
    {
        Ticker = ticker;
        Date = date;
        Open = open;
        High = high;
        Low = low;
        Close = close;
        Volume = volume;
    }
}
