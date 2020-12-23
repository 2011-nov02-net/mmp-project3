using DataAccess.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories {
    public class TradeRepository : ITradeRepository {

        private readonly DbContextOptions<mmpproject2Context> _contextOptions;

        public TradeRepository(DbContextOptions<mmpproject2Context> contextOptions) {
            _contextOptions = contextOptions;
        }

        public async Task<IEnumerable<Domain.Models.Trade>> GetAllAsync() {
            using var context = new mmpproject2Context(_contextOptions);
            var trades = await context.Trades
                .Include(t => t.Stock)
                .ToListAsync();

            return trades.Select(Mapper.MapTrade);
        }

        public async Task<IEnumerable<Domain.Models.Trade>> GetAllAsync(Domain.Models.Portfolio portfolio) {
            using var context = new mmpproject2Context(_contextOptions);
            var trades = await context.Trades.Where(t => t.PortfolioId == portfolio.Id)
                .Include(t => t.Stock)
                .ToListAsync();

            return trades.Select(Mapper.MapTrade);
        }

        public async Task<Domain.Models.Trade> GetAsync(int id) {
            using var context = new mmpproject2Context(_contextOptions);
            var trade = await context.Trades
                .Include(t => t.Stock)
                .FirstAsync(t => t.Id == id);

            return Mapper.MapTrade(trade);
        }

        public async Task<Domain.Models.Trade> AddAsync(Domain.Models.Trade trade, Domain.Models.Portfolio portfolio) {
            using var context = new mmpproject2Context(_contextOptions);
            var dbPortfolio = await context.Portfolios
                .Include(p => p.Trades)
                .FirstAsync(p => p.Id == portfolio.Id);
            var newTrade = Mapper.MapTrade(trade);

            dbPortfolio.Trades.Add(newTrade);
            context.Trades.Add(newTrade);

            await context.SaveChangesAsync();

            return Mapper.MapTrade(newTrade);
        }

        public async Task UpdateAsync(Domain.Models.Trade trade) {
            using var context = new mmpproject2Context(_contextOptions);
            var current = await context.Trades.FirstAsync(t => t.Id == trade.Id);
            var updated = Mapper.MapTrade(trade);

            context.Entry(current).CurrentValues.SetValues(updated);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            using var context = new mmpproject2Context(_contextOptions);
            var tr = await context.Trades.FirstAsync(t => t.Id == id);

            context.Remove(tr);

            await context.SaveChangesAsync();
        }
    }
}
