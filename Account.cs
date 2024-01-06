namespace SimpleStockMarketSimulation
{
    public class Account
    {
        public decimal Balance { get; private set; }
        public readonly Exchange Exchange;
        public List<Position> Positions { get; private set; } = new List<Position>();
        public List<Position> ClosedPositions { get; set; } = new List<Position>();
        public Account(decimal balance, Exchange exchange)
        {
            Balance = balance;
            Exchange = exchange;
        }

        public void Buy(Order order)
        {
            if (order.Euro > Balance)
            {
                Console.WriteLine("Not enough money to buy!");
                return;
            }

            Position position = new Position(this, order.Euro, order.Leverage, order.StopLoss);
            Positions.Add(position);
        }

        public void ChangeBalance(decimal amount, bool income)
        {
            if (income)
                Balance += amount;
            else
                Balance -= amount;
        }
    }
}