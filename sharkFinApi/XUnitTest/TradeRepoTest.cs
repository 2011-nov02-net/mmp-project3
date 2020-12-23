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
        [Fact]
        public async Task AddTrade_Database_TestAsync()
        {
            using var connection = new SqliteConnection("Data Source=:memory");
            connection.Open();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            var stock = new Domain.Models.Stock("APPL", "NASDAQ", "Apple Inc.", null);
            testTrade = new Domain.Models.Trade(null,10,1002.50m, DateTime.Now);

            using (var context = new mmpproject2Context(options))
            {
                context.Database.EnsureCreated();
                var repo = new TradeRepository(options);
                await repo.AddAsync(testTrade, null);
            }
            using var context2 = new mmpproject2Context(options);
            DataAccess.Models.Trade tradeReal = context2.Trades
                .Single(t => t.Stock.Name == "Apple Inc.");

            Assert.Equal(testTrade.Stock.Name, tradeReal.Stock.Name);
            Assert.Equal(testTrade.Stock.Symbol, tradeReal.Stock.Symbol);
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
                Assert.Contains(trade.Stock.Name, tradesActual.Select(x => x.Stock.Name));
                Assert.Contains(trade.Stock.Id, tradesActual.Select(x => x.Stock.Id));
                Assert.Contains(trade.Price, tradesActual.Select(x => x.Price));
                Assert.Contains(trade.Quantity, tradesActual.Select(x => x.Quantity));
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task GetTradebyID_Database_test(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new TradeRepository(options);

            var trade = await repo.GetAsync(id);

            var tradeActual = context.Trades.Where(x => x.Id == id).Single();

            Assert.Equal(trade.Id, tradeActual.Id);
            Assert.Equal(trade.Stock.Id, tradeActual.Stock.Id);
            Assert.Equal(trade.Stock.Name, tradeActual.Stock.Name);
            Assert.Equal(trade.Price, tradeActual.Price);
            Assert.Equal(trade.Quantity, tradeActual.Quantity);
        }

    }
}
