using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
    public class TradeTest : IDisposable 
    {
        Trade testTrade;
        public TradeTest()
        {
            var stock = new Stock("APPL", "NASDAQ", "Apple", null);
            testTrade = new Trade(stock, 5, 565.25m, DateTime.Now);

        }

        [Fact]
        public void UserTest1()
        {

            //Act
            testTrade.Quantity = 3;


            //Assert
            Assert.Equal(3, testTrade.Quantity);
        }

        [Fact]
        public void UserTest2()
        {
            //Arrange

            //Act
            testTrade.Price = 123.23m;

            //Assert
            Assert.Equal(123.23m, testTrade.Price);
        }

        public void Dispose()
        {
            testTrade = null;
        }
    }
}
