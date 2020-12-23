using DataAccess.Models;
using Domain.Interfaces;
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
            var portfolios = await context.Portfolios
                .Include(p => p.Assets)
                    .ThenInclude(a => a.Stock)
                .Include(p => p.Trades)
                    .ThenInclude(t => t.Stock)
                .ToListAsync();

            return portfolios.Select(Mapper.MapPortfolio);
        }

        public async Task<IEnumerable<Domain.Models.Portfolio>> GetAllAsync(Domain.Models.User user) {
            using var context = new mmpproject2Context(_contextOptions);
            var portfolios = await context.Portfolios
                .Where(p => p.UserId == user.Id)
                .Include(p => p.Assets)
                    .ThenInclude(a => a.Stock)
                .Include(p => p.Trades)
                    .ThenInclude(t => t.Stock)
                .ToListAsync();

            return portfolios.Select(Mapper.MapPortfolio);
        }

        public async Task<Domain.Models.Portfolio> GetAsync(int id) {
            using var context = new mmpproject2Context(_contextOptions);
            var portfolio = await context.Portfolios
                .Include(p => p.Assets)
                    .ThenInclude(a => a.Stock)
                .Include(p => p.Trades)
                    .ThenInclude(t => t.Stock)
                .FirstAsync(p => p.Id == id);

            return Mapper.MapPortfolio(portfolio);
        }

        public async Task<Domain.Models.Portfolio> AddAsync(Domain.Models.Portfolio portfolio, Domain.Models.User user) {
            if (portfolio.Id != 0) {
                throw new ArgumentException("Portfolio already exists.");
            }

            using var context = new mmpproject2Context(_contextOptions);
            var dbUser = await context.Users
                .Include(u => u.Portfolios)
                .FirstAsync(u => u.Id == user.Id);
            var newPortfolio = Mapper.MapPortfolio(portfolio);

            dbUser.Portfolios.Add(newPortfolio);
            context.Portfolios.Add(newPortfolio);

            await context.SaveChangesAsync();

            return Mapper.MapPortfolio(newPortfolio);
        }

        public async Task UpdateAsync(Domain.Models.Portfolio portfolio) {
            using var context = new mmpproject2Context(_contextOptions);
            var current = await context.Portfolios.FindAsync(portfolio.Id);
            var updated = Mapper.MapPortfolio(portfolio);

            updated.UserId = current.UserId;
            context.Entry(current).CurrentValues.SetValues(updated);

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
