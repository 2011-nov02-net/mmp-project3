using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess {
    public static class Mapper {
        
        /// <summary>
        /// Maps an EntityFramework user to a business model
        /// </summary>
        /// <param name="user">The DataAccess user object</param>
        /// <returns>The user business model</returns>
        public static Domain.Models.User MapUser(Models.User user) {
            return new Domain.Models.User(user.FirstName,
                user.LastName,
                user.Email,
                user.UserName,
                user.Portfolios.Select(MapPortfolio).ToHashSet()) {
                Id = user.Id
            };
        }

        /// <summary>
        /// Maps a business domain user to an EntityFramework model
        /// </summary>
        /// <param name="user">The business domain user object</param>
        /// <returns>The user DataAccess object</returns>
        public static Models.User MapUser(Domain.Models.User user) {
            return new Models.User {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName
            };
        }

        /// <summary>
        /// Maps an EntityFramework portfolio to a business model
        /// </summary>
        /// <param name="portfolio">The DataAccess portfolio object</param>
        /// <returns>The portfolio business model</returns>
        public static Domain.Models.Portfolio MapPortfolio(Models.Portfolio portfolio) {
            return new Domain.Models.Portfolio(
                portfolio.Name,
                portfolio.Funds,
                portfolio.PortfolioEntries.Select(MapAsset).ToHashSet(),
                portfolio.Trades.Select(MapTrade).ToHashSet()) {
                Id = portfolio.Id
            };
        }

        /// <summary>
        /// Maps a business domain portfolio to an EntityFramework model
        /// </summary>
        /// <param name="portfolio">The business domain portfolio object</param>
        /// <returns>The portfolio DataAccess object</returns>
        public static Models.Portfolio MapPortfolio(Domain.Models.Portfolio portfolio) {
            return new Models.Portfolio {
                Name = portfolio.Name,
                Funds = portfolio.Funds
            };
        }

        /// <summary>
        /// Maps an EntityFramework asset to a business model
        /// </summary>
        /// <param name="asset">The DataAccess portfolio entry object</param>
        /// <returns>The asset business model</returns>
        public static Domain.Models.Asset MapAsset(Models.PortfolioEntry asset) {
            return new Domain.Models.Asset(
                MapPortfolio(asset.Portfolio),
                MapStock(asset.Stock),
                asset.Quantity);
        }

        /// <summary>
        /// Maps a business domain asset to an EntityFramework model
        /// </summary>
        /// <param name="asset">The business domain asset object</param>
        /// <returns>The portfolio entry DataAccess object</returns>
        public static Models.PortfolioEntry MapAsset(Domain.Models.Asset asset) {
            return new Models.PortfolioEntry {
                Portfolio = MapPortfolio(asset.Portfolio),
                Stock = MapStock(asset.Stock),
                Quantity = asset.Quantity
            };
        }

        /// <summary>
        /// Maps an EntityFramework trade to a business model
        /// </summary>
        /// <param name="trade">The DataAccess trade object</param>
        /// <returns>The trade business model</returns>
        public static Domain.Models.Trade MapTrade(Models.Trade trade) {
            return new Domain.Models.Trade(
                MapPortfolio(trade.Portfolio),
                MapStock(trade.Stock),
                trade.Quantity,
                trade.Price,
                trade.TimeTraded) {
                Id = trade.Id
            };
        }

        /// <summary>
        /// Maps a business domain trade to an EntityFramework model
        /// </summary>
        /// <param name="trade">The business domain trade object</param>
        /// <returns>The trade DataAccess object</returns>
        public static Models.Trade MapTrade(Domain.Models.Trade trade) {
            return new Models.Trade {
                Portfolio = MapPortfolio(trade.Portfolio),
                Stock = MapStock(trade.Stock),
                Quantity = trade.Quantity,
                Price = trade.Price,
                TimeTraded = trade.Time
            };
        }

        /// <summary>
        /// Maps an EntityFramework stock to a business model
        /// </summary>
        /// <param name="stock">The DataAccess stock object</param>
        /// <returns>The stock business model</returns>
        public static Domain.Models.Stock MapStock(Models.Stock stock) {
            return new Domain.Models.Stock(
                stock.Symbol,
                stock.Market,
                stock.Name,
                stock.Logo);
        }

        /// <summary>
        /// Maps a business domain stock to an EntityFramework model
        /// </summary>
        /// <param name="stock">The business domain stock object</param>
        /// <returns>The stock DataAccess object</returns>
        public static Models.Stock MapStock(Domain.Models.Stock stock) {
            return new Models.Stock {
                Symbol = stock.Symbol,
                Market = stock.Market,
                Name = stock.Name,
                Logo = stock.Logo
            };
        }
    }
}
