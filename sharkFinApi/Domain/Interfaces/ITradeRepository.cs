using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces {
    public interface ITradeRepository {
        Task<IEnumerable<Trade>> GetAllAsync();
        Task<IEnumerable<Trade>> GetAllAsync(Portfolio portfolio);
        Task<Trade> GetAsync(int id);
        Task<Trade> AddAsync(Trade trade, Portfolio portfolio);
        Task UpdateAsync(Trade trade);
        Task DeleteAsync(int id);
    }
}
