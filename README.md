# MultiFactor Portfolio Engine

A modular and extensible **multi-factor quantitative research and
backtesting framework** built using **C# / .NET 10**.\
This repository provides a clean, industry-aligned architecture for
structuring quantitative trading systems, enabling you to build signals,
construct portfolios, run backtests, and analyse performance in a
production-quality way.

------------------------------------------------------------------------

## 📌 Project Purpose

This project serves as a **professional template** for:

-   Building factor-based strategies\
-   Constructing portfolios (long-only and long-short)\
-   Running simulation-quality backtests\
-   Performing risk & performance analytics\
-   Extending into ML-driven research using Python (optional)

It is designed to reflect how real quant funds structure their internal
research stacks.

------------------------------------------------------------------------

## 🧱 Architecture Overview

    Data Layer
        - Price downloaders (Yahoo/AV)
        - Data store (CSV/SQL)
        - Market data models

    Factor Engine
        - IFactor interface
        - Momentum, Volatility, Mean Reversion, Quality
        - Custom research factors

    Portfolio Layer
        - Z-scoring & normalization
        - Composite scoring
        - Ranking & weighting
        - Portfolio construction (LO/LS)

    Backtesting Engine
        - Event-driven daily loop
        - PnL calculations
        - Slippage/fees hooks
        - Trade logs

    Analytics
        - Sharpe, Sortino, Max Drawdown
        - Rolling volatility & correlation
        - Visualisation hooks

    Future (Optional)
        - Blazor dashboard
        - Optimization (risk parity / Markowitz)
        - ML notebooks (Python)

------------------------------------------------------------------------

## 📂 Solution Structure

    MultiFactorPortfolioEngine.sln
    │
    ├── QuantEngine/                # Core quant library
    │   ├── Data/
    │   ├── Factors/
    │   ├── Portfolio/
    │   ├── Backtesting/
    │   ├── Analytics/
    │   └── Common/
    │
    └── QuantEngine.Tests/          # xUnit test project

------------------------------------------------------------------------

## 🚀 Getting Started

### 1. Clone the repository:

``` bash
git clone https://github.com/dakshalraijada/MultiFactorPortfolioEngine.git
cd MultiFactorPortfolioEngine
```

### 2. Build the solution:

``` bash
dotnet build
```

### 3. Run tests:

``` bash
dotnet test
```

Implementation will be added incrementally as part of the project
roadmap.

------------------------------------------------------------------------

## 🧪 Testing (xUnit)

This solution uses **xUnit** for all test projects to align with modern
.NET and quant engineering standards.

Tests will cover: - Factor behaviour\
- Portfolio construction logic\
- Backtest engine correctness\
- Statistical and performance calculations

------------------------------------------------------------------------

## 🧭 Roadmap

### Phase 1 --- Core Engine

-   [ ] Price downloader (Yahoo/AV)\
-   [ ] Data store layer\
-   [ ] Base factor library\
-   [ ] Portfolio scorer + ranker\
-   [ ] Backtest engine v1

### Phase 2 --- Analytics

-   [ ] Rolling volatility & correlation\
-   [ ] Performance metrics & summary report\
-   [ ] Equity curve chart

### Phase 3 --- Research Enhancements

-   [ ] Long-short support\
-   [ ] Execution & slippage models\
-   [ ] Optimization models\
-   [ ] Python research notebook

------------------------------------------------------------------------

## 📜 License

MIT License *(recommended)*

------------------------------------------------------------------------

## 🤝 Contributions

Contributions, ideas, and improvements are welcome.\
This project is intended as a learning and exploration platform for
quant developers using C#.
