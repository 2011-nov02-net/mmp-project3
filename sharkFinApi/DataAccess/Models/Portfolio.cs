using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models {
    public partial class Portfolio
    {
        public Portfolio()
        {
            Assets = new HashSet<Asset>();
            Trades = new HashSet<Trade>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public decimal Funds { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<Trade> Trades { get; set; }
    }
}
