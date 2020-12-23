using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
    public partial class UnitTest
    {
        Domain.Models.Trade testTrade;
        Domain.Models.Portfolio portfolio;
        Domain.Models.User user;
        [Fact]
        public async Task AddTrade_Database_TestAsync()
        {
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            var stock = new Domain.Models.Stock("APPL", "NASDAQ", "Apple Inc.", "Logo");
            testTrade = new Domain.Models.Trade(stock,10,1002.50m, DateTime.Now);
            portfolio = new Domain.Models.Portfolio("TEst", 3000.0m, null, null);
            user = new Domain.Models.User("Matt","Goodman","mg@gmail.com","mg", null);

            using (var context = new mmpproject2Context(options))
            {
                context.Database.EnsureCreated();
                var repo = new TradeRepository(options);
                var portRepo = new PortfolioRepository(options);
                var userRepo = new UserRepository(options);
                await userRepo.AddAsync(user);
                await portRepo.AddAsync(portfolio, user);
                await repo.AddAsync(testTrade, portfolio);
            }
            using var context2 = new mmpproject2Context(options);
            DataAccess.Models.Trade tradeReal = context2.Trades
                .Single(t => t.Stock.Name == "Apple Inc.");

            Assert.Equal(testTrade.Id, tradeReal.Id);
            Assert.Equal(testTrade.Price, tradeReal.Price);
        }
        [Fact]
        public async Task GetTrades_Database_testAsync()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new TradeRepository(options);

            var trades = await repo.GetAllAsync();
            var tradesActual = context.Trades.ToList();

            foreach (var trade in trades)
            {
                Assert.Contains(trade.Id, tradesActual.Select(x => x.Id));
                Assert.Contains(trade.Price, tradesActual.Select(x => x.Price));
                Assert.Contains(trade.Quantity, tradesActual.Select(x => x.Quantity));
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task GetTradebyID_Database_test(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new TradeRepository(options);

            var trade = await repo.GetAsync(id);

            var tradeActual = context.Trades.Where(x => x.Id == id).Single();

            Assert.Equal(trade.Id, tradeActual.Id);
            Assert.Equal(trade.Price, tradeActual.Price);
            Assert.Equal(trade.Quantity, tradeActual.Quantity);
        }

    }
}
