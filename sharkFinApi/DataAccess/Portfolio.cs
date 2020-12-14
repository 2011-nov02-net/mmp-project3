using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess
{
    public partial class Portfolio
    {
        public int UserId { get; set; }
        public string StockSymbol { get; set; }
        public string StockMarket { get; set; }
        public int Quantity { get; set; }

        public virtual Stock Stock { get; set; }
        public virtual User User { get; set; }
    }
}
