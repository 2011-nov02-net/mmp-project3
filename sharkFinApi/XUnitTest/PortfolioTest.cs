using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
    public class PortfolioTest : IDisposable
    {
        Portfolio testPortfolio;
        public PortfolioTest()
        {
            testPortfolio = new Portfolio
            {
                Name = "Apple",
                Funds = 565.25m,
            };
        }

        [Fact]
        public void UserTest1()
        {

            //Act
            testPortfolio.Name = "Tesla";


            //Assert
            Assert.Equal("Tesla", testPortfolio.Name);
        }

        [Fact]
        public void UserTest2()
        {
            //Arrange

            //Act
            testPortfolio.Funds = 123.23m;

            //Assert
            Assert.Equal(123.23m, testPortfolio.Funds);
        }

        public void Dispose()
        {
            testPortfolio = null;
        }
    }
}
