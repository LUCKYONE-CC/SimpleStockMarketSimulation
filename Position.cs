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
        public Position(Account account, decimal investedMoney, int leverage, decimal stopLoss = 0)
        {
            PositionId = Id;
            Id++;
            InvestedMoney = investedMoney;
            Leverage = leverage;
            Shares = investedMoney * leverage / account.Exchange.StockPrice;
            account.ChangeBalance(investedMoney + account.Exchange.TransactionFee, false);
            FromAccount = account;
            StopLoss = stopLoss;

            if(stopLoss != 0)
            {
                account.Exchange.GetPricePublisher().InteractionReceived += HandleStopLoss;
            }
        }

        public void Sell(Account account, bool fromStopLoss = false)
        {
            decimal borrowedMoney = Leverage * InvestedMoney;
            decimal currentValue;

            currentValue = Shares * account.Exchange.StockPrice;

            if (fromStopLoss)
            {
                currentValue = Shares * StopLoss;
            }

            decimal profit = (currentValue - borrowedMoney) - account.Exchange.TransactionFee;

            Profit = profit;

            account.ChangeBalance(InvestedMoney, true);
            account.ChangeBalance(profit, true);
            Close();

            Console.WriteLine($"Position {PositionId} closed with profit of {profit}€");
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
                Sell(FromAccount, true);
            }
        }
    }
}