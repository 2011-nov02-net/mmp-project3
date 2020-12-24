

namespace Domain.Models {
    public class Asset {

        public int Id { get; set; }
        public Stock Stock { get; set; }
        public int Quantity { get; set; }

        public Asset(Stock stock, int quantity) {
            Stock = stock;
            Quantity = quantity;
        }
    }
}
