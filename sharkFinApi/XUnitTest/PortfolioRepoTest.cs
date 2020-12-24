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
        public async Task GetPortfolios_Database_testAsync()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new PortfolioRepository(options);

            var portfolios = await repo.GetAllAsync();
            var portfoliosActual = context.Portfolios.ToList();

            foreach (var portfolio in portfolios)
            {
                Assert.Contains(portfolio.Id, portfoliosActual.Select(x => x.Id));
                Assert.Contains(portfolio.Name, portfoliosActual.Select(x => x.Name));
                Assert.Contains(portfolio.Funds, portfoliosActual.Select(x => x.Funds));
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task GetPortfoliobyID_Database_test(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new PortfolioRepository(options);

            var portfolio = await repo.GetAsync(id);

            var portfolioActual = context.Portfolios.Where(x => x.Id == id).Single();

            Assert.Equal(portfolio.Id, portfolioActual.Id);
            Assert.Equal(portfolio.Name, portfolioActual.Name);
            Assert.Equal(portfolio.Funds, portfolioActual.Funds);

        }

    }
}
