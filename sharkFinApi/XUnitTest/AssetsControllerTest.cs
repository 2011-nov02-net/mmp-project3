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
        static readonly Mock<IAssetRepository> _assetMock = new Mock<IAssetRepository>();
        static readonly Mock<ILogger<AssetsController>> _loggerAssetMock = new Mock<ILogger<AssetsController>>();
        static readonly AssetsController assetsController = new AssetsController(_assetMock.Object, _loggerAssetMock.Object);

        [Fact]
        public async Task AssetsController_GetAssets()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new AssetRepository(options);
            var actionResult = await assetsController.GetAsync();
            var assets = await repo.GetAllAsync();
            var assetsActual = context.Assets.ToList();

            foreach (var asset in assets)
            {
                Assert.Contains(asset.Id, assetsActual.Select(x => x.Id));
                Assert.Contains(asset.Quantity, assetsActual.Select(x => x.Quantity));
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task AssetsController_GetAssetbyID(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new AssetRepository(options);
            var actionResult = await assetsController.GetByIdAsync(id);
            var asset = await repo.GetAsync(id);

            var assetActual = context.Assets.Where(x => x.Id == id).Single();

            Assert.Equal(asset.Id, assetActual.Id);
            Assert.Equal(asset.Quantity, assetActual.Quantity);
        }
    }
}
