namespace SimpleStockMarketSimulation
{
    public delegate void InteractionHandler(object sender, decimal currentPrice, bool priceIncreased);
    public class ExchangePricePublisher
    {
        public event InteractionHandler InteractionReceived;

        public async Task SendInteraction(decimal currentPrice, bool priceIncreased)
        {
            if (InteractionReceived != null)
            {
                InteractionReceived(this, currentPrice, priceIncreased);
            }
        }
    }
}
