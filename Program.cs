using System;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStockMarketSimulation
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Exchange exchange = new Exchange(100);
            Account account = new Account(110, exchange);

            exchange.OpenExchange();

            exchange.GetPricePublisher().InteractionReceived += (sender, price, priceIncreased) =>
            {
                //if (priceIncreased)
                //    Console.ForegroundColor = ConsoleColor.Green;
                //else
                //    Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine($"Aktueller Preis: {Math.Round(price, 2)}€, Preis gestiegen: {priceIncreased}");
                //Console.ResetColor();
            };

            account.Buy(new Order(100, 0, 2));
            account.Buy(new Order(100, 0, 1));

            Console.ReadKey();

            account.Positions[0].Sell(account);
            account.Positions[0].Sell(account);

            Console.ReadKey();

            exchange.CloseExchange();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.ResetColor();
        }
    }
}
