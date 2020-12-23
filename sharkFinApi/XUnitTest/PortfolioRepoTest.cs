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


            using (var context = new mmpproject2Context(options))
            {
                context.Database.EnsureCreated();
                var repo = new PortfolioRepository(options);

                await repo.AddAsync(testPortfolio,testUser);
            }

            using var context2 = new mmpproject2Context(options);
            DataAccess.Models.Portfolio testReal = context2.Portfolios
                .Single(l => l.Name == "Matt");

            Assert.Equal(testPortfolio.Id, testReal.Id);
            Assert.Equal(testPortfolio.Name, testReal.Name);
        }
    }
}
