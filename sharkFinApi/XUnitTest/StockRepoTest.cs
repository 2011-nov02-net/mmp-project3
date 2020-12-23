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
        Domain.Models.Stock testStock;
        [Fact]
        public async Task AddStock_Database_TestAsync()
        {
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            testStock = new Domain.Models.Stock("APPL", "NASDAQ", "Apple Inc.", null);


            using (var context = new mmpproject2Context(options))
            {
                context.Database.EnsureCreated();
                var repo = new StockRepository(options);

                await repo.AddAsync(testStock);
            }

            using var context2 = new mmpproject2Context(options);
            DataAccess.Models.Stock testReal = context2.Stocks
                .Single(l => l.Symbol == "APPL");

            Assert.Equal(testStock.Symbol, testReal.Symbol);
            Assert.Equal(testStock.Name, testReal.Name);
        }
        [Fact]
        public async Task GetStocks_Database_testAsync()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new StockRepository(options);

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
        public async Task GetStockbyID_Database_test(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new StockRepository(options);

            var stock = await repo.GetAsync(id);

            var stockActual = context.Stocks.Where(x => x.Id == id).Single();

           
            Assert.Equal(stock.Name, stockActual.Name);
            Assert.Equal(stock.Symbol, stockActual.Symbol);
            Assert.Equal(stock.Market, stockActual.Market);
            

        }
    }
}
