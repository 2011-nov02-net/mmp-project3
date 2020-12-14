using Stock_Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IUser
    {
        public IEnumerable<Stock> GetStocks();
        public IEnumerable<Stock> GetStockByName(string name);
        public IEnumerable<User> GetUserLeading(int id);

        public void AddUser(User user);
        public void AddPortfolio(PortfolioEntry portfolioEntry);
        public void SellStocks(Portfolio portfolio);
    }
}
