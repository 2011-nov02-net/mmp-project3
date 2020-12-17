using DataAccess.Models;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Stock = Domain.Models.Stock;

namespace Domain.Repositories
{
    public class StockRepo : IStockRepository
    {
        private readonly DbContextOptions<mmpproject2Context> _contextOptions;

        public StockRepo(DbContextOptions<mmpproject2Context> contextOptions)
        {
            _contextOptions = contextOptions;
        }
        public Task Add(Stock stock)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Stock stock)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Stock> GetOneStock(string symbol, string market)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetStockByMarket(string market)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetStockBySymbol(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task Update(Stock stock)
        {
            throw new NotImplementedException();
        }
    }
}
