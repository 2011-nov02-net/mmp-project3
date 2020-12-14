﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess
{
    public partial class Trade
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string StockSymbol { get; set; }
        public string StockMarket { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime TimeTraded { get; set; }

        public virtual Stock Stock { get; set; }
        public virtual User User { get; set; }
    }
}
