namespace SimpleStockMarketSimulation
{
    public abstract class TradingStrategy
    {
        protected Account Account;
        protected Exchange Exchange;

        public TradingStrategy(Account account, Exchange exchange)
        {
            Account = account;
            Exchange = exchange;

            Exchange.GetPricePublisher().InteractionReceived += EvaluateMarketChanges;
        }
        protected abstract void EvaluateMarketChanges(object sender, decimal currentPrice, bool priceIncreased);

        public void DisplayPerformanceStatistics()
        {
            int totalTrades = Account.ClosedPositions.ToList().Count;
            decimal totalProfit = Account.ClosedPositions.Sum(i => i.Profit);

            decimal averageProfitPerTrade = totalTrades > 0 ? totalProfit / totalTrades : 0;

            Console.WriteLine($"Total number of trades: {totalTrades}");
            Console.WriteLine($"Total profit/loss: {Math.Round(totalProfit, 2)}€");
            Console.WriteLine($"Average profit/loss per trade: {Math.Round(averageProfitPerTrade, 2)}€");
            Console.WriteLine($"Account balance: {Math.Round(Account.Balance, 2)}€");
        }
    }
}