using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models {
    public class Trade {

        public int Id { get; set; }
        public Portfolio Portfolio { get; set; }
        public Stock Stock { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Time { get; set; }

        public Trade(Portfolio portfolio, Stock stock, int qty, decimal price, DateTime time) {
            Portfolio = portfolio;
            Stock = stock;
            Quantity = qty;
            Price = price;
            Time = time;
        }
        public Trade()
        {

        }

    }
}
