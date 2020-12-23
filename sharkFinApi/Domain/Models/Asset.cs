using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models {
    public class Asset {

        public int Id { get; set; }
        public Stock Stock { get; set; }
        public int Quantity { get; set; }

        public Asset(Stock stock, int qty) {
            Stock = stock;
            Quantity = qty;
        }
    }
}
