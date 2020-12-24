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
        static readonly Mock<ITradeRepository> _tradesMock = new Mock<ITradeRepository>();
        static readonly Mock<IAssetRepository> _assetsMock = new Mock<IAssetRepository>();
        static readonly Mock<IStockRepository> _stocksMock = new Mock<IStockRepository>();
        static readonly Mock<IPortfolioRepository> _portfoliosMock = new Mock<IPortfolioRepository>();
        static readonly Mock<ILogger<PortfoliosController>> _loggersMock = new Mock<ILogger<PortfoliosController>>();
        static readonly PortfoliosController portfoliosController = new PortfoliosController(_portfoliosMock.Object, _assetsMock.Object,_tradesMock.Object,_stocksMock.Object,_loggersMock.Object);

        [Fact]
        public async Task PorfoliosController_GetPortfolios_testAsync()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new PortfolioRepository(options);
            var actionResult = await portfoliosController.GetAsync();
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
        public async Task PortfoliosController_GetPortfoliobyID_test(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new PortfolioRepository(options);
            var actionResult = await portfoliosController.GetByIdAsync(id);
            var portfolio = await repo.GetAsync(id);

            var portfolioActual = context.Portfolios.Where(x => x.Id == id).Single();

            Assert.Equal(portfolio.Id, portfolioActual.Id);
            Assert.Equal(portfolio.Name, portfolioActual.Name);
            Assert.Equal(portfolio.Funds, portfolioActual.Funds);

        }
    }
}
