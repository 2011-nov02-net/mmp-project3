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
        public IEnumerable<Portfolio> GetPortfolios();
        public IEnumerable<Portfolio> GetPortfolioById(int id);


        public void AddUser(User user);
        public void AddPortfolio(PortfolioEntry portfolioEntry);
        public void SellStocks(Portfolio portfolio);
        public void EditUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(User user);
        public void AddFund(Portfolio portfolio);
        public void AddEntry(PortfolioEntry portfolioEntry);
    }
}
