using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces {
    public interface IStockRepository {
        
        Task<IEnumerable<Stock>> GetAllAsync();
        Task<IEnumerable<Stock>> GetAllBySymbolAsync(string symbol);
        Task<IEnumerable<Stock>> GetAllByMarketAsync(string market);
        Task<Stock> GetAsync(int id);
        Task<Stock> GetAsync(string symbol, string market);
        Task AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task DeleteAsync(string symbol, string market);
    }
}
