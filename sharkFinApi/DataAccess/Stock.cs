using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess
{
    public partial class Stock
    {
        public Stock()
        {
            PortfolioEntries = new HashSet<PortfolioEntry>();
            Trades = new HashSet<Trade>();
        }

        public string Symbol { get; set; }
        public string Market { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<PortfolioEntry> PortfolioEntries { get; set; }
        public virtual ICollection<Trade> Trades { get; set; }
    }
}
