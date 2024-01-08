namespace SimpleStockMarketSimulation
{
    public class Position
    {
        private static int Id { get; set; } = 1;
        private Account FromAccount { get; set; }
        public int PositionId { get; private set; }
        public int Leverage { get; private set; }
        public decimal InvestedMoney { get; private set; }
        public decimal Shares { get; private set; }
        public decimal StopLoss { get; private set; }
        public bool Closed { get; private set; } = false;
        public decimal Profit { get; private set; }
        public Position(Account account, decimal investedMoney, int leverage = 0, decimal stopLoss = 0)
        {
            PositionId = Id;
            Id++;
            InvestedMoney = investedMoney;
            Leverage = leverage;

            account.ChangeBalance(investedMoney + account.Exchange.TransactionFee, false);

            if (leverage > 0)
            {
                investedMoney *= leverage;
            }

            Shares = investedMoney / account.Exchange.StockPrice;
            FromAccount = account;
            StopLoss = stopLoss;

            if(stopLoss != 0)
            {
                account.Exchange.GetPricePublisher().InteractionReceived += HandleStopLoss;
            }
        }

        public void Sell(Account account)
        {
            if (Closed)
            {
                throw new InvalidOperationException("Cannot sell a closed position.");
            }

            decimal currentStockPrice = account.Exchange.StockPrice;
            decimal saleAmount = Shares * currentStockPrice;

            decimal initialAmount = InvestedMoney * (Leverage > 0 ? Leverage : 1);
            Profit = saleAmount - initialAmount;

            account.ChangeBalance((Profit + InvestedMoney) - account.Exchange.TransactionFee, true);

            Close();

            //Console.WriteLine($"Position {PositionId} closed with profit of {Math.Round(Profit, 2)}€");
        }
        private void Close()
        {
            Closed = true;
            FromAccount.Positions.Remove(this);
            FromAccount.ClosedPositions.Add(this);
        }
        private void HandleStopLoss(object sender, decimal currentPrice, bool priceIncreased)
        {
            if(Closed)
            {
                FromAccount.Exchange.GetPricePublisher().InteractionReceived -= HandleStopLoss;
                return;
            }

            if (currentPrice <= StopLoss)
            {
                Sell(FromAccount);
            }
        }
    }
}