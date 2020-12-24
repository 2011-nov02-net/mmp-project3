using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces {
    public interface IPortfolioRepository {
        Task<IEnumerable<Portfolio>> GetAllAsync();
        Task<IEnumerable<Portfolio>> GetAllAsync(User user);
        Task<Portfolio> GetAsync(int id);
        Task<Portfolio> AddAsync(Portfolio portfolio, User user);
        Task UpdateAsync(Portfolio portfolio);
        Task DeleteAsync(int id);
    }
}
