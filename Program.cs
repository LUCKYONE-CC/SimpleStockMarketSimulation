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
            Account account = new Account(1000, exchange); // Startguthaben von 110 Euro

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

            Console.ReadKey();

            // Schließen der Börse
            exchange.CloseExchange();

            Console.ForegroundColor = ConsoleColor.Blue;
            myStrategy.DisplayPerformanceStatistics();
            Console.ResetColor();
        }
    }
}
