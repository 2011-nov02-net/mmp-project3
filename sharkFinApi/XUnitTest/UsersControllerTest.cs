using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using sharkFinApi.Controllers;
using Domain.Interfaces;
using Moq;
using Xunit;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;

namespace XUnitTest
{
    public partial class UnitTest
    {

        static readonly Mock<IUserRepository> _userMock = new Mock<IUserRepository>();
        static readonly Mock<IPortfolioRepository> _portfolioMock = new Mock<IPortfolioRepository>();
        static readonly Mock<ILogger<UsersController>> _loggerMock = new Mock<ILogger<UsersController>>();  
        static readonly UsersController usersController = new UsersController(_userMock.Object, _portfolioMock.Object, _loggerMock.Object);
        
        [Fact]
        public async Task UserController_GetAllAsync()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new UserRepository(options);

            //Act
            var actionResult = await usersController.GetAsync();
            var users = await repo.GetAllAsync();
            var usersActual = context.Users.ToList();
            //Assert
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
        public async Task UserController_GetUserbyID(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new UserRepository(options);
            var actionResult = await usersController.GetByIdAsync(id);
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
        public async Task UserController_GetUserbyEmail(string email)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new UserRepository(options);
            var actionResult = await usersController.GetByEmailAsync(email);
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
