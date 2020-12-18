using DataAccess.Models;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DataAccess.Repositories {
    public class PortfolioRepository : IPortfolioRepository {

        private readonly DbContextOptions<mmpproject2Context> _contextOptions;

        public PortfolioRepository(DbContextOptions<mmpproject2Context> contextOptions) {
            _contextOptions = contextOptions;
        }

        public async Task<IEnumerable<Domain.Models.Portfolio>> GetAllAsync() {
            using var context = new mmpproject2Context(_contextOptions);
            var portfolios = await context.Portfolios.ToListAsync();

            return portfolios.Select(Mapper.MapPortfolio);
        }

        public async Task<IEnumerable<Domain.Models.Portfolio>> GetAllAsync(Domain.Models.User user) {
            using var context = new mmpproject2Context(_contextOptions);
            var portfolios = await context.Portfolios.Where(p => p.UserId == user.Id).ToListAsync();

            return portfolios.Select(Mapper.MapPortfolio);
        }

        public async Task<Domain.Models.Portfolio> GetAsync(int id) {
            using var context = new mmpproject2Context(_contextOptions);
            var portfolio = await context.Portfolios
                .Include(p => p.PortfolioEntries)
                    .ThenInclude(a => a.Stock)
                .Include(p => p.Trades)
                    .ThenInclude(t => t.Stock)
                .FirstAsync(p => p.Id == id);

            return Mapper.MapPortfolio(portfolio);
        }

        public async Task AddAsync(Domain.Models.Portfolio portfolio) {
            if (portfolio.Id != 0) {
                throw new ArgumentException("Portfolio already exists.");
            }

            using var context = new mmpproject2Context(_contextOptions);
            var dbPortfolio = Mapper.MapPortfolio(portfolio);

            await context.Portfolios.AddAsync(dbPortfolio);

            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Models.Portfolio portfolio) {
            using var context = new mmpproject2Context(_contextOptions);
            var current = await context.Portfolios.FindAsync(portfolio.Id);
            var updated = Mapper.MapPortfolio(portfolio);

            context.Entry(current).CurrentValues.SetValues(updated);

            foreach (var asset in portfolio.Assets) {
                var pe = await context.PortfolioEntries.FirstOrDefaultAsync(a => a.PortfolioId == portfolio.Id && a.StockSymbol == asset.Stock.Symbol && a.StockMarket == asset.Stock.Market);
                var newPE = Mapper.MapAsset(asset);
                if (pe is null) {
                    await context.PortfolioEntries.AddAsync(newPE);
                } else {
                    context.Entry(pe).CurrentValues.SetValues(newPE);
                }
            }

            foreach (var trade in portfolio.Trades) {
                var tr = await context.Trades.FirstOrDefaultAsync(t => t.PortfolioId == portfolio.Id && t.StockSymbol == trade.Stock.Symbol && t.StockMarket == trade.Stock.Market);
                var newTrade = Mapper.MapTrade(trade);
                if (tr is null) {
                    await context.Trades.AddAsync(newTrade);
                } else {
                    context.Entry(tr).CurrentValues.SetValues(newTrade);
                }
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            using var context = new mmpproject2Context(_contextOptions);
            var portfolio = await context.Portfolios.FindAsync(id);

            context.Remove(portfolio);

            await context.SaveChangesAsync();
        }

    }
}
