using DataAccess.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DataAccess.Repositories {
    public class AssetRepository : IAssetRepository {

        private readonly DbContextOptions<mmpproject2Context> _contextOptions;

        public AssetRepository(DbContextOptions<mmpproject2Context> contextOptions) {
            _contextOptions = contextOptions;
        }

        public async Task<IEnumerable<Domain.Models.Asset>> GetAllAsync() {
            using var context = new mmpproject2Context(_contextOptions);
            var assets = await context.Assets
                .Include(a => a.Stock)
                .ToListAsync();

            return assets.Select(Mapper.MapAsset);
        }

        public async Task<IEnumerable<Domain.Models.Asset>> GetAllAsync(Domain.Models.Portfolio portfolio) {
            using var context = new mmpproject2Context(_contextOptions);
            var assets = await context.Assets.Where(a => a.PortfolioId == portfolio.Id)
                .Include(a => a.Stock)
                .ToListAsync();
            
            return assets.Select(Mapper.MapAsset);
        }

        public async Task<Domain.Models.Asset> GetAsync(int id) {
            using var context = new mmpproject2Context(_contextOptions);
            var asset = await context.Assets
                .Include(a => a.Stock)
                .FirstAsync(a => a.Id == id);

            return Mapper.MapAsset(asset);
        }

        public async Task<Domain.Models.Asset> AddAsync(Domain.Models.Asset asset, Domain.Models.Portfolio portfolio) {
            using var context = new mmpproject2Context(_contextOptions);
            var dbPortfolio = await context.Portfolios
                .Include(p => p.Assets)
                .FirstAsync(p => p.Id == portfolio.Id);
            var newAsset = Mapper.MapAsset(asset);

            dbPortfolio.Assets.Add(newAsset);
            context.Assets.Add(newAsset);

            await context.SaveChangesAsync();

            return Mapper.MapAsset(newAsset);
        }

        public async Task UpdateAsync(Domain.Models.Asset asset) {
            using var context = new mmpproject2Context(_contextOptions);
            var current = await context.Assets.FirstAsync(a => a.Id == asset.Id);
            var updated = Mapper.MapAsset(asset);

            context.Entry(current).CurrentValues.SetValues(updated);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            using var context = new mmpproject2Context(_contextOptions);
            var ast = await context.Assets.FirstAsync(a => a.Id == id);

            context.Remove(ast);

            await context.SaveChangesAsync();
        }
    }
}
