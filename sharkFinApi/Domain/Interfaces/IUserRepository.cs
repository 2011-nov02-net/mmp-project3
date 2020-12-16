using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces {
    public interface IUserRepository {
        
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUserByName(string firstName, string lastName);
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task Add(User user);
        Task Update(User user);
        Task Delete(User user);
    }
}
