using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
    public partial class UnitTest
    {
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
