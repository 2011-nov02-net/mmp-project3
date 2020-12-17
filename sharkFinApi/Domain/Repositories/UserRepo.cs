using DataAccess.Models;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = Domain.Models.User;

namespace Domain.Repositories
{
    public class UserRepo : IUserRepository
    {
        private readonly DbContextOptions<mmpproject2Context> _contextOptions;
        public UserRepo(DbContextOptions<mmpproject2Context> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public async Task<User> Add(User user)
        {
            using var context = new mmpproject2Context(_contextOptions);
            var newUser = new DataAccess.Models.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
            };
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Delete(int id)
        {
            using var context = new mmpproject2Context(_contextOptions);
            try
            {
                var _user = await context.Users.FindAsync(id);
                context.Users.Remove(_user);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                Console.WriteLine("User not found");
                return false;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using var context = new mmpproject2Context(_contextOptions);
            var dbUsers = await context.Users.ToListAsync();
            if (dbUsers == null) return null;
            var users = dbUsers.Select(u => new User
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                UserName = u.UserName
            });
            return users;
        }

        public async Task<DataAccess.Models.User> GetUserByEmail(string email)
        {
            using var context = new mmpproject2Context(_contextOptions);
            var user = await context.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
            
            return user;
        }

        public Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByName(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(int id, User user)
        {
            using var context = new mmpproject2Context(_contextOptions);

            try
            {
                var _user = await context.Users.FindAsync(id);
                {
                    _user.FirstName = user.FirstName;
                    _user.LastName = user.LastName;
                    if(!String.IsNullOrEmpty(user.Email))
                        _user.Email = user.Email;
                    _user.UserName = user.UserName;
                }
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
