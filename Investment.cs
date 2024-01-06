namespace SimpleStockMarketSimulation
{
    public class Investment
    {
        private static int Id { get; set; } = 1;
        public int InvestmentId { get; private set; } = Id;
        public int Amount { get; private set; }
        public decimal StopLoss { get; private set; }
        public int Leverage { get; private set; }
        public decimal PriceAtBuyTime { get; private set; }
        public decimal ClosingPrice { get; private set; }
        public bool Closed { get; private set; }
        public Investment(decimal stockPrice, int amount, decimal stopLoss, int leverage)
        {
            PriceAtBuyTime = stockPrice;
            Amount = amount;
            StopLoss = stopLoss;
            Leverage = leverage;
            InvestmentId = Id;
            Id++;
        }

        public void Close(decimal closingPrice)
        {
            Closed = true;
            ClosingPrice = closingPrice;
        }
    }
}
