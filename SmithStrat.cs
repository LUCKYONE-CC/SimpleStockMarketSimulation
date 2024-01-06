namespace SimpleStockMarketSimulation
{
    public class SmithStrat : TradingStrategy
    {
        private decimal initialPrice;
        private const int leverage = 125;
        private const decimal stopLossMargin = 0.01m; // 1 Cent
        private const decimal sellThresholdIncrease = 0.0025m; // 0.25% Steigerung
        private int currentInvestmentId;

        public SmithStrat(Account account, Exchange exchange)
            : base(account, exchange)
        {
        }

        protected override async void EvaluateMarketChanges(object sender, decimal currentPrice, bool priceIncreased)
        {
            // Wenn keine Position offen ist, öffne eine neue Position
            if (Account.Investments.Count == 0)
            {
                initialPrice = currentPrice;
                decimal stopLoss = initialPrice - currentPrice;
                currentInvestmentId = await BuyStock(1, stopLoss, leverage); // Hier wird eine Menge von 1 angenommen, kann angepasst werden
            }
            else
            {
                // Prüfe, ob maximal 0.25% gestiegen ist
                if (currentPrice >= initialPrice * (1 + sellThresholdIncrease))
                {
                    await SellStock(currentInvestmentId); // Verkaufe die gesamte Position
                }
            }
        }
    }
}
