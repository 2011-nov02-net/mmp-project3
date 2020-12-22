using DataAccess.Models;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTest
{
    public partial class UnitTest
    {
        public SqliteConnection Database_init()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;

            User[] users =
            {
                new User
                {
                    FirstName = "Rosel",
                    LastName = "Pardillo",
                    Email = "rosel@gmail.com",
                    UserName = "roselP",
                    Id = 1 
                },
                new User
                {
                    FirstName = "Rodrigo",
                    LastName = "Duterte",
                    Email = "rody@gmail.com",
                    UserName = "rodyDuterte",
                    Id = 2
                },
                new User
                {
                    FirstName = "Grace",
                    LastName = "Libardos",
                    Email = "graceLibardos@gmail.com",
                    UserName = "graceLibards",
                    Id = 3
                },
                new User
                {
                    FirstName = "Jose",
                    LastName = "Rizal",
                    Email = "joseRizal@gmail.com",
                    UserName = "joseRiz",
                    Id = 4
                },
                new User
                {
                    FirstName = "Mary",
                    LastName = "Grace",
                    Email = "mG@gmail.com",
                    UserName = "maryGrace",
                    Id = 5
                },
            };

            Stock[] stocks =
            {
                new Stock
                {
                    Symbol = "XXX",
                    Market = "S&P",
                    Name = "XtraXtra"
                },
                new Stock
                {
                    Symbol = "TSLA",
                    Market = "NASDAQ",
                    Name = "TESLA"
                },
                new Stock
                {
                    Symbol = "ABS",
                    Market = "PH",
                    Name = "ABS-CBN"
                },
                new Stock
                {
                    Symbol = "GMA",
                    Market = "PH",
                    Name = "MEDIA"
                },
                new Stock
                {
                    Symbol = "PBB",
                    Market = "PH",
                    Name = "BigBrother"
                },
                new Stock
                {
                    Symbol = "JNJ",
                    Market = "NYSE",
                    Name = "Johnson & Johnson"
                }

            };

            Trade[] trades =
            {
                new Trade
                {
                    Id = 1,
                    PortfolioId = 1,
                    StockSymbol = "TSLA",
                    StockMarket = "NASDAQ",
                    Quantity = 10,
                    Price = 500.12m
                },
                new Trade
                {
                    Id = 3,
                    PortfolioId = 3,
                    StockSymbol = "ABS",
                    StockMarket = "PH",
                    Quantity = 10,
                    Price = 11.12m
                },
                new Trade
                {
                    Id = 4,
                    PortfolioId = 4,
                    StockSymbol = "GMA",
                    StockMarket = "PH",
                    Quantity = 10,
                    Price = 7.12m
                },
                new Trade
                {
                    Id = 5,
                    PortfolioId = 5,
                    StockSymbol = "PBB",
                    StockMarket = "PH",
                    Quantity = 10,
                    Price = 20.12m
                },
                new Trade
                {
                    Id = 6,
                    PortfolioId = 6,
                    StockSymbol = "JNJ",
                    StockMarket = "NYSE",
                    Quantity = 10,
                    Price = 40.12m
                },


            };

            Portfolio[] portfolios =
            {
                new Portfolio
                {
                    Id = 1,
                    Name = "Matt",
                    UserId = 1,
                    Funds = 3000
                },
                new Portfolio
                {
                    Id = 2,
                    Name = "GOOD",
                    UserId = 2,
                    Funds = 3000
                },
                new Portfolio
                {
                    Id = 3,
                    Name = "Marnien",
                    UserId = 3,
                    Funds = 3000
                },
                new Portfolio
                {
                    Id = 4,
                    Name = "Paul",
                    UserId = 4,
                    Funds = 3000
                },
                new Portfolio
                {
                    Id = 5,
                    Name = "Best",
                    UserId = 5,
                    Funds = 3000
                },
            };

            PortfolioEntry[] entries =
            {
                new PortfolioEntry
                {
                    PortfolioId = 1,
                    StockSymbol = "XXX",
                    StockMarket = "S&P",
                    Quantity = 10
                },
                new PortfolioEntry
                {
                    PortfolioId = 2,
                    StockSymbol = "TSLA",
                    StockMarket = "NASDAQ",
                    Quantity = 10
                },
                new PortfolioEntry
                {
                    PortfolioId = 3,
                    StockSymbol = "ABS",
                    StockMarket = "PH",
                    Quantity = 10
                },
                new PortfolioEntry
                {
                    PortfolioId = 4,
                    StockSymbol = "GMA",
                    StockMarket = "PH",
                    Quantity = 10
                },
                new PortfolioEntry
                {
                    PortfolioId = 5,
                    StockSymbol = "PBB",
                    StockMarket = "PH",
                    Quantity = 10
                },
            };



            var context = new mmpproject2Context(options);
            context.Database.EnsureCreated();
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            foreach (var stock in stocks)
            {
                context.Stocks.Add(stock);
            }
            foreach (var trade in trades)
            {
                context.Trades.Add(trade);
            }
            foreach (var portfolio in portfolios)
            {
                context.Portfolios.Add(portfolio);
            }
            foreach (var entry in entries)
            {
                context.PortfolioEntries.Add(entry);
            }

            context.SaveChanges();

            return connection;
        }
    }
}
