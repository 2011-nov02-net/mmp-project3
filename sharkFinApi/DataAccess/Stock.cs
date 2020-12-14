using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess
{
    public partial class Stock
    {
        public Stock()
        {
            Portfolios = new HashSet<Portfolio>();
            Trades = new HashSet<Trade>();
        }

        public string Symbol { get; set; }
        public string Market { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Portfolio> Portfolios { get; set; }
        public virtual ICollection<Trade> Trades { get; set; }
    }
}
