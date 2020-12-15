using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Domain.Interfaces {
    public interface IStockRepository {
        ICollection<Stock> GetAll();
        ICollection<Stock> GetAllBySymbol(string symbol);
        ICollection<Stock> GetAllByMarket(string market);
        Stock Get(string symbol, string market);
        void Add(Stock stock);
        void Update(Stock stock);
        void Delete(Stock stock);
    }
}
