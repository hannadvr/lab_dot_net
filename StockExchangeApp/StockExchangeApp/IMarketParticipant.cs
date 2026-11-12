namespace StockExchangeApp
{
    public interface IMarketParticipant
    {
        string Name { get; }
        string OnMarketClosed();
    }
}