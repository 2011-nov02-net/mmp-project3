using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class PortfolioEntry
    {
        public int PortfolioId { get; set; }
        public string StockSymbol { get; set; }
        public string StockMarket { get; set; }
        public int Quantity { get; set; }

        public virtual Portfolio Portfolio { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
