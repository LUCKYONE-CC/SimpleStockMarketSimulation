namespace SimpleStockMarketSimulation
{
    public class SmithStrat : TradingStrategy
    {
        private decimal initialPrice;
        private const int leverage = 125;
        private const decimal stopLossMargin = 0.01m; // 1 Cent
        private const decimal sellThresholdIncrease = 0.0025m; // 0.025% Steigerung
        private int currentInvestmentId;

        public SmithStrat(Account account, Exchange exchange)
            : base(account, exchange)
        {
        }

        protected override void EvaluateMarketChanges(object sender, decimal currentPrice, bool priceIncreased)
        {
            // Wenn keine Position offen ist, öffne eine neue Position
            if (Account.Positions.Count == 0)
            {
                initialPrice = currentPrice;
                decimal stopLoss = currentPrice;
                Account.Buy(new Order(100 - (Account.Exchange.TransactionFee * 2), stopLoss, leverage));
            }
            else
            {
                // Prüfe, ob maximal 0.025% gestiegen ist
                if (currentPrice >= initialPrice * (1 + sellThresholdIncrease))
                {
                    Account.Positions[0].Sell(Account);
                }
            }
        }
    }
}