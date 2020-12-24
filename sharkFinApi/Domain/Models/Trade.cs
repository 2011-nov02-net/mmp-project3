using System;


namespace Domain.Models {
    public class Trade {

        public int Id { get; set; }
        public Stock Stock { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Time { get; set; }

        public Trade(Stock stock, int quantity, decimal price, DateTime time) {
            Stock = stock;
            Quantity = quantity;
            Price = price;
            Time = time;
        }


    }
}
