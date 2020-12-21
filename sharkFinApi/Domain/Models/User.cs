using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models {
    public class User {

        private readonly ICollection<Portfolio> _portfolios;
        public IReadOnlyCollection<Portfolio> Portfolios => new HashSet<Portfolio>(_portfolios);

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public User()
        {

        }
        public User(string firstName, string lastName, string email, string username, ICollection<Portfolio> portfolios) {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = username;
            _portfolios = portfolios ?? new HashSet<Portfolio>();
        }

        // TODO:
        //  - Implement user-related methods such as adding/deleting a portfolio

    }
}
