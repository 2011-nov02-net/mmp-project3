using DataAccess.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<Domain.Models.Stock> GetAsync(int id) {
            using var context = new mmpproject2Context(_contextOptions);
            var stock = await context.Stocks.FirstAsync(s => s.Id == id);

            return Mapper.MapStock(stock);
        }

        public async Task<Domain.Models.Stock> GetAsync(string symbol) {
            using var context = new mmpproject2Context(_contextOptions);
            var stock = await context.Stocks.Where(s => s.Symbol.Contains(symbol)).FirstAsync();

            return Mapper.MapStock(stock);
        }

        public async Task<Domain.Models.Stock> AddAsync(Domain.Models.Stock stock) {
            if (stock.Id != 0) {
                throw new ArgumentException("Stock already exists.");
            }

            using var context = new mmpproject2Context(_contextOptions);
            var newStock = Mapper.MapStock(stock);

            await context.Stocks.AddAsync(newStock);

            await context.SaveChangesAsync();

            return Mapper.MapStock(newStock);
        }

        public async Task UpdateAsync(Domain.Models.Stock stock) {
            using var context = new mmpproject2Context(_contextOptions);
            var current = await context.Stocks.FirstAsync(s => s.Symbol == stock.Symbol && s.Market == stock.Market);
            var updated = Mapper.MapStock(stock);

            context.Entry(current).CurrentValues.SetValues(updated);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            using var context = new mmpproject2Context(_contextOptions);
            var dbStock = await context.Stocks.FirstAsync(s => s.Id == id);

            context.Remove(dbStock);

            await context.SaveChangesAsync();
        }
                
    }
}
