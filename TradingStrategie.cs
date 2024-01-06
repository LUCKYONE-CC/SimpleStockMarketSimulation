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

        // Diese Methode wird aufgerufen, wenn der Aktienkurs sich ändert.
        protected abstract void EvaluateMarketChanges(object sender, decimal currentPrice, bool priceIncreased);

        // Methode zum Ausführen des Kaufs von Aktien
        protected async Task<int> BuyStock(int amount, decimal stopLoss = 0, int leverage = 1)
        {
            return await Account.BuyStock(amount, stopLoss, leverage);
        }

        // Methode zum Ausführen des Verkaufs von Aktien
        protected async Task SellStock(int investmentId)
        {
            await Account.SellStock(investmentId);
        }

        public void DisplayPerformanceStatistics()
        {
            int totalTrades = Account.ClosedInvestments.Count;
            decimal totalProfit = Account.ClosedInvestments.Sum(investment =>
                investment.Leverage * investment.Amount * (investment.ClosingPrice - investment.PriceAtBuyTime));

            decimal averageProfitPerTrade = totalTrades > 0 ? totalProfit / totalTrades : 0;

            Console.WriteLine($"Gesamtanzahl der Trades: {totalTrades}");
            Console.WriteLine($"Gesamtgewinn/verlust: {Math.Round(totalProfit, 2)}€");
            Console.WriteLine($"Durchschnittlicher Gewinn/Verlust pro Trade: {Math.Round(averageProfitPerTrade, 2)}€");
            Console.WriteLine($"Account-Balance: {Math.Round(Account.Balance, 2)}€");
        }

        // Möglicherweise weitere gemeinsame Methoden und Eigenschaften für Handelsstrategien
    }
}
