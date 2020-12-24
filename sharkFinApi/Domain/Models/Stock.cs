

namespace Domain.Models {
    public class Stock {

        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Market { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }

        public Stock(string symbol, string market, string name, string logo) {
            Symbol = symbol;
            Market = market;
            Name = name;
            Logo = logo;
        }
    }
}
