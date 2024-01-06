namespace SimpleStockMarketSimulation
{
    public class Account
    {
        public decimal Balance { get; private set; }
        public List<Investment> Investments { get; private set; } = new List<Investment>();
        public List<Investment> ClosedInvestments { get; private set; } = new List<Investment>();
        public readonly Exchange Exchange;
        private decimal TransactionFee = 1m;
        public Account(decimal balance, Exchange exchange)
        {
            Balance = balance;
            Exchange = exchange;

        }
        public async Task<int> BuyStock(int amount, decimal stopLoss = 0, int leverage = 1)
        {
            if(stopLoss > amount * Exchange.StockPrice)
            {
                throw new Exception("Stop loss is higher than the amount of money you want to invest!");
            }

            if (Balance >= Exchange.StockPrice * amount + TransactionFee)
            {
                Balance -= Exchange.StockPrice * amount - TransactionFee;
                Investment investment = new Investment(Exchange.StockPrice, amount, stopLoss, leverage);
                Investments.Add(investment);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Investment opened at {Math.Round(Exchange.StockPrice, 2)}€");
                Console.ResetColor();

                if (stopLoss > 0)
                {
                    Exchange.GetPricePublisher().InteractionReceived += async (sender, price, priceIncreased) =>
                    {
                        if (investment == null || investment.Closed)
                            return;

                        if (price <= investment.StopLoss)
                        {
                            await SellStock(investment.InvestmentId, investment.StopLoss);
                        }
                    };
                }

                return investment.InvestmentId;
            }
            throw new Exception("Not enough money to buy stock!");
        }
        public async Task SellStock(int investmentId, decimal closingPrice = 0)
        {
            if (Investments.Count > 0)
            {
                //Closingprice ist dazu da, um die Position da zu schließen, wo der tatsächliche potenzielle Stop-Loss-Wert liegt
                if(closingPrice == 0)
                    closingPrice = Exchange.StockPrice;
                Investment investment = Investments.Find(i => i.InvestmentId == investmentId);

                if(investment == null)
                    throw new Exception("Investment not found!");

                Investments.RemoveAt(0);
                decimal profit = investment.Leverage * investment.Amount * (closingPrice - investment.PriceAtBuyTime);
                Balance += profit - TransactionFee;
                investment.Close(closingPrice);
                ClosedInvestments.Add(investment);

                if (profit > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine($"Gewinn: {Math.Round(profit, 2)}€");
                Console.ResetColor();
            }
        }
    }
}