using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces {
    public interface IPortfolioRepository {
        Task<IEnumerable<Portfolio>> GetAllAsync();
        Task<IEnumerable<Portfolio>> GetAllAsync(User user);
        Task<Portfolio> GetAsync(int id);
        Task<Portfolio> AddAsync(Portfolio portfolio);
        Task UpdateAsync(Portfolio portfolio);
        Task DeleteAsync(int id);
    }
}
