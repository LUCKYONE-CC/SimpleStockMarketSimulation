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
            Exchange exchange = new Exchange(50); // Startpreis von 1000 Euro
            Account account = new Account(200, exchange); // Startguthaben von 10000 Euro

            // Öffnen der Börse
            exchange.OpenExchange();

            // Erstellen und Starten der Handelsstrategie
            TradingStrategy myStrategy = new SmithStrat(account, exchange);

            // Optionale Konsolenausgabe für Preisänderungen (kann entfernt werden)
            exchange.GetPricePublisher().InteractionReceived += (sender, price, priceIncreased) =>
            {
                //Console.WriteLine($"Aktueller Preis: {Math.Round(price, 2)}€, Preis gestiegen: {priceIncreased}");
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