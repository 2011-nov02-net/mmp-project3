using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Domain.Interfaces {
    public interface IUserRepository {
        ICollection<User> GetAll();
        ICollection<User> GetAll(string firstName, string lastName);
        User Get(int id);
        User Get(string email);
        void Add(User user);
        void Update(User user);
        void Delete(User user);
    }
}
