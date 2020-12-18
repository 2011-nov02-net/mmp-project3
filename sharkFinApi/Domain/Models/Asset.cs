using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models {
    public class Asset {

        public Portfolio Portfolio { get; set; }
        public Stock Stock { get; set; }
        public int Quantity { get; set; }

        public Asset(Portfolio portfolio, Stock stock, int qty) {
            Portfolio = portfolio;
            Stock = stock;
            Quantity = qty;
        }
    }
}
