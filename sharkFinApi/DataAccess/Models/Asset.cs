using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models {
    public partial class Asset
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }

        public virtual Portfolio Portfolio { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
