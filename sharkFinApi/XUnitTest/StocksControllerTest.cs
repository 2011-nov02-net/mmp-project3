using DataAccess.Models;
using DataAccess.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using sharkFinApi.Controllers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
   public partial class UnitTest
    {
        static readonly Mock<IStockRepository> _stockMock = new Mock<IStockRepository>();
        static readonly Mock<ILogger<StocksController>> _loggerStockMock = new Mock<ILogger<StocksController>>();
        static readonly StocksController stocksController = new StocksController(_stockMock.Object, _loggerStockMock.Object);
        [Fact]
        public async Task StocksController_GetStocks()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new StockRepository(options);

            var actionResult = await stocksController.GetAsync();
            var stocks = await repo.GetAllAsync();
            var stocksActual = context.Stocks.ToList();

            foreach (var stock in stocks)
            {
                Assert.Contains(stock.Symbol, stocksActual.Select(x => x.Symbol));
                Assert.Contains(stock.Name, stocksActual.Select(x => x.Name));
                Assert.Contains(stock.Id, stocksActual.Select(x => x.Id));
                Assert.Contains(stock.Market, stocksActual.Select(x => x.Market));
                Assert.Contains(stock.Logo, stocksActual.Select(x => x.Logo));
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task StocksController_GetStockbyID_test(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new StockRepository(options);
            var actionResult = await stocksController.GetByIdAsync(id);
            var stock = await repo.GetAsync(id);

            var stockActual = context.Stocks.Where(x => x.Id == id).Single();


            Assert.Equal(stock.Name, stockActual.Name);
            Assert.Equal(stock.Symbol, stockActual.Symbol);
            Assert.Equal(stock.Market, stockActual.Market);
        }
        [Theory]
        [InlineData("XXX")]
        [InlineData("TSLA")]
        [InlineData("ABS")]
        [InlineData("GMA")]
        [InlineData("PBB")]
        public async Task StocksController_GetStockbySymbol(string symbol)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new StockRepository(options);
            var actionResult = await stocksController.GetBySymbolAsync(symbol);
            var stock = await repo.GetAsync(symbol);

            var stockActual = context.Stocks.Where(x => x.Symbol == symbol).Single();


            Assert.Equal(stock.Name, stockActual.Name);
            Assert.Equal(stock.Symbol, stockActual.Symbol);
            Assert.Equal(stock.Market, stockActual.Market);
        }
    }
}
