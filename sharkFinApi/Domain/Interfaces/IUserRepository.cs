using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces {
    public interface IUserRepository {
        
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> GetUserByName(string firstName, string lastName);
        Task<User> GetUserById(int id);
        Task<DataAccess.Models.User> GetUserByEmail(string email);
        Task<User> Add(User user);
        Task<bool> Update(int id, User user);
        Task<bool> Delete(int id);
    }
}
