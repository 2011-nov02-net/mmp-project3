using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces {
    public interface IAssetRepository {
        Task<IEnumerable<Asset>> GetAllAsync();
        Task<IEnumerable<Asset>> GetAllAsync(Portfolio portfolio);
        Task<Asset> GetAsync(int id);
        Task<Asset> AddAsync(Asset asset, Portfolio portfolio);
        Task UpdateAsync(Asset asset);
        Task DeleteAsync(int id);
    }
}
