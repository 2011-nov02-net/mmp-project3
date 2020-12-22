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

        public async Task<IEnumerable<Domain.Models.Asset>> GetAllAsync(Domain.Models.Portfolio portfolio) {
            using var context = new mmpproject2Context(_contextOptions);
            var assets = await context.PortfolioEntries.Where(a => a.PortfolioId == portfolio.Id)
                .Include(a => a.Stock)
                .ToListAsync();
            
            return assets.Select(Mapper.MapAsset);
        }

        public async Task<Domain.Models.Asset> GetAsync(Domain.Models.Portfolio portfolio, Domain.Models.Stock stock) {
            using var context = new mmpproject2Context(_contextOptions);
            var asset = await context.PortfolioEntries
                .Include(a => a.Stock)
                .FirstAsync(a => a.PortfolioId == portfolio.Id && a.StockSymbol == stock.Symbol && a.StockMarket == stock.Market);

            return Mapper.MapAsset(asset);
        }

        public async Task AddAsync(Domain.Models.Asset asset, Domain.Models.Portfolio portfolio) {
            using var context = new mmpproject2Context(_contextOptions);
            var dbPortfolio = await context.Portfolios
                .Include(p => p.PortfolioEntries)
                .FirstAsync(p => p.Id == portfolio.Id);
            var newAsset = Mapper.MapAsset(asset);

            dbPortfolio.PortfolioEntries.Add(newAsset);
            context.PortfolioEntries.Add(newAsset);

            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Models.Asset asset, Domain.Models.Portfolio portfolio) {
            using var context = new mmpproject2Context(_contextOptions);
            var current = await context.PortfolioEntries.FirstAsync(a => a.PortfolioId == portfolio.Id && a.StockSymbol == asset.Stock.Symbol && a.StockMarket == asset.Stock.Market);
            var updated = Mapper.MapAsset(asset);

            context.Entry(current).CurrentValues.SetValues(updated);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Domain.Models.Asset asset, Domain.Models.Portfolio portfolio) {
            using var context = new mmpproject2Context(_contextOptions);
            var pe = await context.PortfolioEntries.FirstAsync(a => a.PortfolioId == portfolio.Id && a.StockSymbol == asset.Stock.Symbol && a.StockMarket == asset.Stock.Market);

            context.Remove(pe);

            await context.SaveChangesAsync();
        }
    }
}
