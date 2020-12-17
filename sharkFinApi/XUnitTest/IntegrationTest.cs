using DataAccess.Models;
using DataAccess.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;


namespace XUnitTest
{
    public class IntegrationTest
    {
        
        private readonly DbContextOptions<mmpproject2Context> _contextOptions;
 
        public IntegrationTest(DbContextOptions<mmpproject2Context> context)
        {
            _contextOptions = context;

        }

        [Fact]
        public async Task Test1()
        {
            bool expected = true;
            bool actual;
            var options = new DbContextOptionsBuilder<mmpproject2Context>()
            .UseInMemoryDatabase(databaseName: "")
            .Options;

            IUserRepository service = null;

            // Run the test against one instance of the context
            using (var context = new mmpproject2Context(options))
            {
                //service = new UserRepository(_contextOptions);
                //actual = service.();
            }
            //Assert.True(expected == actual);
        }

    }
}
