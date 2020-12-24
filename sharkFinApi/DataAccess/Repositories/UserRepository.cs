using DataAccess.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories {
    public class UserRepository : IUserRepository {

        private readonly DbContextOptions<mmpproject2Context> _contextOptions;


        public UserRepository(DbContextOptions<mmpproject2Context> contextOptions) {
            _contextOptions = contextOptions;
        }

        public async Task<IEnumerable<Domain.Models.User>> GetAllAsync() {
            using var context = new mmpproject2Context(_contextOptions);
            var users = await context.Users
                .Include(u => u.Portfolios)
                .ToListAsync();

            return users.Select(Mapper.MapUser);
        }

        public async Task<IEnumerable<Domain.Models.User>> GetAllAsync(string firstName, string lastName) {
            using var context = new mmpproject2Context(_contextOptions);
            var users = await context.Users
                .Where(u => u.FirstName == firstName && u.LastName == lastName)
                .Include(u => u.Portfolios)
                .ToListAsync();

            return users.Select(Mapper.MapUser);
        }

        public async Task<Domain.Models.User> GetAsync(int id) {
            using var context = new mmpproject2Context(_contextOptions);
            var user = await context.Users
                .Include(u => u.Portfolios)
                .FirstAsync(u => u.Id == id);

            return Mapper.MapUser(user);
        }

        public async Task<Domain.Models.User> GetAsync(string email) {
            using var context = new mmpproject2Context(_contextOptions);
            var user = await context.Users
                .Include(u => u.Portfolios)
                .FirstAsync(u => u.Email == email);

            return Mapper.MapUser(user);
        }

        public async Task<Domain.Models.User> AddAsync(Domain.Models.User user) {
            if (user.Id != 0) {
                throw new ArgumentException("User already exists.");
            }

            using var context = new mmpproject2Context(_contextOptions);
            var newUser = Mapper.MapUser(user);

            await context.Users.AddAsync(newUser);

            await context.SaveChangesAsync();

            return Mapper.MapUser(newUser);
        }

        public async Task UpdateAsync(Domain.Models.User user) {
            using var context = new mmpproject2Context(_contextOptions);
            var current = await context.Users.FindAsync(user.Id);
            var updated = Mapper.MapUser(user);

            context.Entry(current).CurrentValues.SetValues(updated);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            using var context = new mmpproject2Context(_contextOptions);
            var user = await context.Users.FindAsync(id);

            context.Remove(user);

            await context.SaveChangesAsync();
        }
    }
}
