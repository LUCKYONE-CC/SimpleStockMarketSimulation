﻿namespace SimpleStockMarketSimulation
{
    public class Exchange
    {
        private ExchangePricePublisher PricePublisher { get; set; }
        public bool ExchangeIsOpen { get; private set; }
        public decimal StockPrice { get; private set; }
        public readonly decimal TransactionFee = 1m;
        public Exchange(decimal startingPrice)
        {
            StockPrice = startingPrice;
        }
        public void OpenExchange()
        {
            PricePublisher = new ExchangePricePublisher();
            ExchangeIsOpen = true;
            Task.Run(async () =>
            {
                decimal lastPrice = StockPrice;
                decimal drift = 0.0001m; // Erwarteter Ertrag der Aktie
                decimal volatility = 0.01m; // Volatilität der Aktie
                Random random = new Random();

                while (ExchangeIsOpen)
                {
                    // Zufällige Änderung, basierend auf der Normalverteilung
                    double randomFactor = Math.Sqrt(-2.0 * Math.Log(random.NextDouble())) * Math.Sin(2.0 * Math.PI * random.NextDouble());

                    // Aktualisieren des Aktienkurses gemäß dem GBM-Modell
                    decimal changePercent = drift + volatility * (decimal)randomFactor;
                    StockPrice *= 1 + changePercent;

                    bool priceIncreased = StockPrice > lastPrice;
                    await PricePublisher.SendInteraction(StockPrice, priceIncreased);
                    lastPrice = StockPrice;

                    await Task.Delay(100);
                }
            });
        }
        public ExchangePricePublisher GetPricePublisher()
        {
            if(PricePublisher != null)
                return PricePublisher;
            throw new Exception("Exchange is not open!");
        }
        public void CloseExchange()
        {
            ExchangeIsOpen = false;
        }
    }
}
