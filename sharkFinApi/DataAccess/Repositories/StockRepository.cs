using DataAccess.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DataAccess.Repositories
{
    public class StockRepository : IStockRepository {

        private readonly DbContextOptions<mmpproject2Context> _contextOptions;

        public StockRepository(DbContextOptions<mmpproject2Context> contextOptions) {
            _contextOptions = contextOptions;
        }

        public async Task<IEnumerable<Domain.Models.Stock>> GetAllAsync() {
            using var context = new mmpproject2Context(_contextOptions);
            var stocks = await context.Stocks.ToListAsync();

            return stocks.Select(Mapper.MapStock);
        }

        public async Task<IEnumerable<Domain.Models.Stock>> GetAllBySymbolAsync(string symbol) {
            using var context = new mmpproject2Context(_contextOptions);
            var stocks = await context.Stocks.Where(s => s.Symbol == symbol).ToListAsync();

            return stocks.Select(Mapper.MapStock);
        }

        public async Task<IEnumerable<Domain.Models.Stock>> GetAllByMarketAsync(string market) {
            using var context = new mmpproject2Context(_contextOptions);
            var stocks = await context.Stocks.Where(s => s.Market == market).ToListAsync();

            return stocks.Select(Mapper.MapStock);
        }

        public async Task<Domain.Models.Stock> GetAsync(string symbol, string market) {
            using var context = new mmpproject2Context(_contextOptions);
            var stock = await context.Stocks.FirstAsync(s => s.Symbol == symbol && s.Market == market);

            return Mapper.MapStock(stock);
        }

        public async Task AddAsync(Domain.Models.Stock stock) {
            using var context = new mmpproject2Context(_contextOptions);
            var dbStock = Mapper.MapStock(stock);

            await context.Stocks.AddAsync(dbStock);

            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Models.Stock stock) {
            using var context = new mmpproject2Context(_contextOptions);
            var current = await context.Stocks.FirstAsync(s => s.Symbol == stock.Symbol && s.Market == stock.Market);
            var updated = Mapper.MapStock(stock);

            context.Entry(current).CurrentValues.SetValues(updated);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string symbol, string market) {
            using var context = new mmpproject2Context(_contextOptions);
            var dbStock = await context.Stocks.FirstAsync(s => s.Symbol == symbol && s.Market == market);

            context.Remove(dbStock);

            await context.SaveChangesAsync();
        }
                
    }
}
