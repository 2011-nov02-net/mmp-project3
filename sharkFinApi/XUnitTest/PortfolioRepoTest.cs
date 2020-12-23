using DataAccess.Models;
using DataAccess.Repositories;
using Domain.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
    
    public partial class UnitTest
    {
        Domain.Models.Portfolio testPortfolio;
        Domain.Models.User testUser;
        [Fact]
        public async Task AddPortfolio_Database_TestAsync()
        {
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            testPortfolio = new Domain.Models.Portfolio("Matt", 3000.0m, null, null);
            testUser = new Domain.Models.User("Matt","Goodman","mg@gmail.com","mg001",null);

            using (var context = new mmpproject2Context(options))
            {
                context.Database.EnsureCreated();
                var repo = new PortfolioRepository(options);
                var userRepo = new UserRepository(options);
                await repo.AddAsync(testPortfolio,testUser);
                await context.SaveChangesAsync();
            }

            using var context2 = new mmpproject2Context(options);
            DataAccess.Models.Portfolio testReal = context2.Portfolios
                .Single(l => l.Id == 1);
            Assert.Equal(testPortfolio.Id, testReal.Id);
            Assert.Equal(testPortfolio.Name, testReal.Name);
        }
        [Fact]
        public async Task GetPortfolios_Database_testAsync()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<mmpproject2Context>().UseSqlite(connection).Options;
            using var context = new mmpproject2Context(options);
            var repo = new PortfolioRepository(options);

            var portfolios = await repo.GetAllAsync();
            var portfoliosActual = context.Portfolios.ToList();

            foreach (var portfolio in portfolios)
            {
                Assert.Contains(portfolio.Id, portfoliosActual.Select(x => x.Id));
                Assert.Contains(portfolio.Name, portfoliosActual.Select(x => x.Name));
                Assert.Contains(portfolio.Funds, portfoliosActual.Select(x => x.Funds));
            }
        }
    }
}
