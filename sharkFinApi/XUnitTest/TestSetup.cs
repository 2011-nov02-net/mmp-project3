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
                    Id = 1,
                    FirstName = "Rosel",
                    LastName = "Pardillo",
                    Email = "rosel@gmail.com",
                    UserName = "roselP",
                    Portfolios = null

                },
                new User
                {
                    Id = 2,
                    FirstName = "Rodrigo",
                    LastName = "Duterte",
                    Email = "rody@gmail.com",
                    UserName = "rodyDuterte",
                    Portfolios = null

                },
                new User
                {
                    Id = 3,
                    FirstName = "Grace",
                    LastName = "Libardos",
                    Email = "graceLibardos@gmail.com",
                    UserName = "graceLibards",
                    Portfolios = null

                },
                new User
                {
                    Id = 4,
                    FirstName = "Jose",
                    LastName = "Rizal",
                    Email = "joseRizal@gmail.com",
                    UserName = "joseRiz",
                    Portfolios = null

                },
                new User
                {
                    Id = 5,
                    FirstName = "Mary",
                    LastName = "Grace",
                    Email = "mG@gmail.com",
                    UserName = "maryGrace",
                    Portfolios = null
                    
                },
            };

            Stock[] stocks =
            {
                new Stock
                {
                    Id = 1,
                    Symbol = "XXX",
                    Market = "S&P",
                    Name = "XtraXtra",
                    Logo = null,
                    Assets = null,
                    Trades = null

                },
                new Stock
                {
                    Id = 2,
                    Symbol = "TSLA",
                    Market = "NASDAQ",
                    Name = "TESLA",
                    Logo = null,
                    Assets = null,
                    Trades = null
                },
                new Stock
                {
                    Id = 3,
                    Symbol = "ABS",
                    Market = "PH",
                    Name = "ABS-CBN",
                    Logo = null,
                    Assets = null,
                    Trades = null
                },
                new Stock
                {
                    Id = 4,
                    Symbol = "GMA",
                    Market = "PH",
                    Name = "MEDIA",
                    Logo = null,
                    Assets = null,
                    Trades = null
                },
                new Stock
                {
                    Id = 5,
                    Symbol = "PBB",
                    Market = "PH",
                    Name = "BigBrother",
                    Logo = null,
                    Assets = null,
                    Trades = null

                },

            };

            Portfolio[] portfolios =
            {
                new Portfolio
                {
                    Id = 1,
                    Name = "Matt",
                    UserId = 1,
                    Funds = 3000.0m,
                    Assets = null,
                    Trades = null,
                    User = null


                },
                new Portfolio
                {
                    Id = 2,
                    Name = "GOOD",
                    UserId = 2,
                    Funds = 3000.0m,
                    Assets = null,
                    Trades = null,
                    User = null

                },
                new Portfolio
                {
                    Id = 3,
                    Name = "Marnien",
                    UserId = 3,
                    Funds = 3000.0m,
                    Assets = null,
                    Trades = null,
                    User = null
                },
                new Portfolio
                {
                    Id = 4,
                    Name = "Paul",
                    UserId = 4,
                    Funds = 3000.0m,
                    Assets = null,
                    Trades = null,
                    User = null
                },
                new Portfolio
                {
                    Id = 5,
                    Name = "Best",
                    UserId = 5,
                    Funds = 3000.0m,
                    Assets = null,
                    Trades = null,
                    User = null
                },
            };


            Trade[] trades =
            {
                new Trade
                {
                    Id = 1,
                    PortfolioId = 1,
                    StockId = 1,
                    Portfolio = null,
                    Stock = null,
                    Quantity = 10,
                    Price = 500.12m,
                    TimeTraded = DateTime.Now
                },
                new Trade
                {
                    Id = 2,
                    PortfolioId = 2,
                    StockId = 2,
                    Portfolio = null,
                    Stock = null,
                    Quantity = 10,
                    Price = 11.12m,
                    TimeTraded = DateTime.Now
                },
                new Trade
                {   
                    Id = 3,
                    PortfolioId = 3,
                    StockId = 3,
                    Portfolio = null,
                    Stock = null,
                    Quantity = 10,
                    Price = 7.12m,
                    TimeTraded = DateTime.Now
                },
                new Trade
                {
                    Id = 4,
                    PortfolioId = 4,
                    StockId = 4,
                    Portfolio = null,
                    Stock = null,
                    Quantity = 10,
                    Price = 20.12m,
                    TimeTraded = DateTime.Now
                },
                new Trade
                {
                    Id = 5,
                    PortfolioId = 5,
                    StockId = 5,
                    Portfolio = null,
                    Stock = null,
                    Quantity = 10,
                    Price = 40.12m,
                    TimeTraded = DateTime.Now
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
            foreach (var portfolio in portfolios)
            {
                context.Portfolios.Add(portfolio);
            }
            foreach (var trade in trades)
            {
                context.Trades.Add(trade);
            }

            context.SaveChanges();

            return connection;
        }
    }
}
