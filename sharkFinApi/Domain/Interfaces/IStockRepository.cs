using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces {
    public interface IStockRepository {
        
        Task<IEnumerable<Stock>> GetAll();
        Task<Stock> GetAllBySymbol(string symbol);
        Task<Stock> GetAllByMarket(string market);
        Task<Stock> GetOneStock(string symbol, string market);
        Task Add(Stock stock);
        Task Update(Stock stock);
        Task Delete(Stock stock);
    }
}
