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

            // Registrierung des Event-Handlers für Preisänderungen
            Exchange.GetPricePublisher().InteractionReceived += EvaluateMarketChanges;
        }
        protected abstract void EvaluateMarketChanges(object sender, decimal currentPrice, bool priceIncreased);

        public void DisplayPerformanceStatistics()
        {
            int totalTrades = Account.ClosedPositions.ToList().Count;
            decimal totalProfit = Account.ClosedPositions.Sum(i => i.Profit);

            decimal averageProfitPerTrade = totalTrades > 0 ? totalProfit / totalTrades : 0;

            Console.WriteLine($"Gesamtanzahl der Trades: {totalTrades}");
            Console.WriteLine($"Gesamtgewinn/verlust: {Math.Round(totalProfit, 2)}€");
            Console.WriteLine($"Durchschnittlicher Gewinn/Verlust pro Trade: {Math.Round(averageProfitPerTrade, 2)}€");
            Console.WriteLine($"Account-Balance: {Math.Round(Account.Balance, 2)}€");
        }
    }
}