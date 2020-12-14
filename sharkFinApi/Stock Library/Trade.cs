using System;
using System.Collections.Generic;
using System.Text;

namespace Stock_Library
{
    public class Trade
    {
        public int Id { get; set; }
        public int PorfolioId { get; set; }
        public string StockSymbol { get; set; }
        public string StockMarket { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime TimeTraded { get; set; }

    }
}
