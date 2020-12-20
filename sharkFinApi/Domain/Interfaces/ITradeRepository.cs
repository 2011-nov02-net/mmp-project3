using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces {
    public interface ITradeRepository {
        Task<IEnumerable<Trade>> GetAllAsync(Portfolio portfolio);
        Task<Trade> GetAsync(Portfolio portfolio, Stock stock);
        Task AddAsync(Trade trade);
        Task UpdateAsync(Trade trade);
        Task DeleteAsync(Trade trade);
    }
}
