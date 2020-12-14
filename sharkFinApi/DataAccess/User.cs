using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess
{
    public partial class User
    {
        public User()
        {
            Portfolios = new HashSet<Portfolio>();
            Trades = new HashSet<Trade>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public decimal Funds { get; set; }

        public virtual ICollection<Portfolio> Portfolios { get; set; }
        public virtual ICollection<Trade> Trades { get; set; }
    }
}
