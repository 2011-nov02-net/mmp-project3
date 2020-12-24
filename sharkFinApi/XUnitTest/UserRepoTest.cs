using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace XUnitTest
{
    public partial class UnitTest
    {
        Domain.Models.User testUser;
        [Fact]
        public async Task AddCustomer_Database_TestAsync()
        {
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            testUser = new Domain.Models.User("Grace","Libardos","gl@gmail.com","gl001", null);


            using (var context = new mmpproject2Context(options))
            {
                context.Database.EnsureCreated();
                var repo = new UserRepository(options);

                await repo.AddAsync(testUser);
                
            }

            using var context2 = new mmpproject2Context(options);
            DataAccess.Models.User userReal = context2.Users
                .Single(l => l.FirstName == "Grace");

            Assert.Equal(testUser.FirstName, userReal.FirstName);
            Assert.Equal(testUser.Email, userReal.Email);
        }
        [Fact]
        public async Task GetUsers_Database_testAsync()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new UserRepository(options);

            var users = await repo.GetAllAsync();
            var usersActual = context.Users.ToList();

            foreach (var user in users)
            {
                Assert.Contains(user.FirstName, usersActual.Select(x => x.FirstName));
                Assert.Contains(user.LastName, usersActual.Select(x => x.LastName));
                Assert.Contains(user.Email, usersActual.Select(x => x.Email));
                Assert.Contains(user.UserName, usersActual.Select(x => x.UserName));
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task GetUserbyID_Database_test(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new UserRepository(options);

            var user = await repo.GetAsync(id);

            var userActual = context.Users.Where(x => x.Id == id).Single();

            Assert.Equal(user.Id, userActual.Id);
            Assert.Equal(user.FirstName, userActual.FirstName);
            Assert.Equal(user.LastName, userActual.LastName);
            Assert.Equal(user.Email, userActual.Email);
            Assert.Equal(user.UserName, userActual.UserName);

        }

        [Theory]
        [InlineData("rosel@gmail.com")]
        [InlineData("rody@gmail.com")]
        [InlineData("graceLibardos@gmail.com")]
        [InlineData("joseRizal@gmail.com")]
        [InlineData("mG@gmail.com")]
        public async Task GetUserbyEmail_Database_test(string email)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new UserRepository(options);

            var user = await repo.GetAsync(email);

            var userActual = context.Users.Where(x => x.Email == email).Single();

            Assert.Equal(user.Id, userActual.Id);
            Assert.Equal(user.FirstName, userActual.FirstName);
            Assert.Equal(user.LastName, userActual.LastName);
            Assert.Equal(user.Email, userActual.Email);
            Assert.Equal(user.UserName, userActual.UserName);
        }


    }
}
