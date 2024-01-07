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

            // Initialisierung der Börse und des Kontos
            Exchange exchange = new Exchange(100); // Startpreis von 100 Euro
            Account account = new Account(100000000000, exchange); // Startguthaben von 110 Euro

            // Öffnen der Börse
            exchange.OpenExchange();

            // Erstellen und Starten der Handelsstrategie
            TradingStrategy myStrategy = new SmithStrat(account, exchange);

            // Optionale Konsolenausgabe für Preisänderungen
            exchange.GetPricePublisher().InteractionReceived += (sender, price, priceIncreased) =>
            {
                //if (priceIncreased)
                //    Console.ForegroundColor = ConsoleColor.Green;
                //else
                //    Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine($"Aktueller Preis: {Math.Round(price, 2)}€, Preis gestiegen: {priceIncreased}");
                //Console.ResetColor();
            };

            //account.Buy(new Order(100, 0, 2));
            //account.Buy(new Order(100, 0, 1));

            //Console.ReadKey();

            //account.Positions[0].Sell(account);
            //account.Positions[0].Sell(account);

            Console.ReadKey();

            // Schließen der Börse
            exchange.CloseExchange();

            Console.ForegroundColor = ConsoleColor.Blue;
            myStrategy.DisplayPerformanceStatistics();
            Console.ResetColor();
        }
    }
}
