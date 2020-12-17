using DataAccess.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class StockRepository : IStockRepository {

        private readonly DbContextOptions<mmpproject2Context> _contextOptions;

        public StockRepository(DbContextOptions<mmpproject2Context> contextOptions) {
            _contextOptions = contextOptions;
        }

        public Task<IEnumerable<Domain.Models.Stock>> GetAll() {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Models.Stock>> GetAllBySymbol(string symbol) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Models.Stock>> GetAllByMarket(string market) {
            throw new NotImplementedException();
        }

        public Task<Domain.Models.Stock> Get(string symbol, string market) {
            throw new NotImplementedException();
        }

        public Task Add(Domain.Models.Stock stock) {
            throw new NotImplementedException();
        }

        public Task Update(Domain.Models.Stock stock) {
            throw new NotImplementedException();
        }

        public Task Delete(Domain.Models.Stock stock) {
            throw new NotImplementedException();
        }
                
    }
}
