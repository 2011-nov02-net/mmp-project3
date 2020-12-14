using System;
using System.Collections.Generic;
using System.Text;

namespace Stock_Library
{
    public class PortfolioEntry
    {
        public int PortfolioId { get; set; }
        public string StockSymbol { get; set; }
        public string StockMarket { get; set; }
        public int Quantity { get; set; }

    }
}
