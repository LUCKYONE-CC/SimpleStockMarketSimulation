# Simple Stock Market Simulation

This project is a simple stock market simulation written in C#. It includes classes for managing an account, an exchange, orders, positions, and a trading strategy. The project simulates a stock exchange with random price fluctuations and allows users to create and manage trading positions. (GBM-Model)

## Table of Contents

- [Features](#features)
- [Usage](#usage)
- [Structure](#structure)
- [Contributing](#contributing)
- [License](#license)

## Features

- Simulates a stock exchange with random price fluctuations.
- Allows users to create and manage trading positions.
- Supports buying and selling of stocks.
- Implements a basic trading strategy (can be extended).

## Usage

To use this stock market simulation, follow these steps:

1. Clone or download the repository.

2. Build the project using Visual Studio or your preferred C# IDE.

3. Run the `Program.cs` file to start the simulation.

4. Follow the on-screen instructions to create accounts, place orders, and monitor the exchange.

5. The simulation will display account balances, open and closed positions, and trading statistics.

## Structure

The project is organized into several C# classes:

- `Account`: Represents a user's trading account with a balance, open and closed positions, and the ability to buy and sell stocks.

- `Exchange`: Represents the stock exchange with price fluctuations and the ability to open and close the exchange.

- `Order`: Represents a trading order with details such as the amount of money to invest, stop loss, and leverage.

- `Position`: Represents a trading position with information about the invested money, leverage, shares, stop loss, and profit.

- `ExchangePricePublisher`: Handles price interactions and events in the exchange.

- `TradingStrategy`: An abstract class that can be extended to implement custom trading strategies.

## Contributing

Contributions to this project are welcome. If you find any issues or have ideas for improvements, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
