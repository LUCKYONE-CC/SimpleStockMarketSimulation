namespace SimpleStockMarketSimulation
{
    public class Order
    {
        public decimal Euro { get; private set; }
        public decimal StopLoss { get; private set; } = 0;
        public int Leverage { get; private set; } = 0;
        public Order(decimal euro, decimal stopLoss = 0, int leverage = 0)
        {
            Euro = euro;
            StopLoss = stopLoss;
            Leverage = leverage;
        }
    }
}
