namespace QuantEngine.Portfolio.Models;

public sealed class FactorScore
{
    public string Ticker { get; }
    public DateTime Date { get; }
    public decimal Score { get; }

    public FactorScore(string ticker, DateTime date, decimal score)
    {
        Ticker = ticker;
        Date = date;
        Score = score;
    }
}
