using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models {
    public partial class Stock
    {
        public Stock()
        {
            Assets = new HashSet<Asset>();
            Trades = new HashSet<Trade>();
        }

        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Market { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<Trade> Trades { get; set; }
    }
}
