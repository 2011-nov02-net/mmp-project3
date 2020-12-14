using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess
{
    public partial class Portfolio
    {
        public Portfolio()
        {
            PortfolioEntries = new HashSet<PortfolioEntry>();
            Trades = new HashSet<Trade>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public decimal Funds { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<PortfolioEntry> PortfolioEntries { get; set; }
        public virtual ICollection<Trade> Trades { get; set; }
    }
}
