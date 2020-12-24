using Domain.Models;
using System;
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

        [Fact]
        public void UserTest3()
        {
            //Arrange

            //Act
            var stock = new Stock("PBB", "PH", "BigBrother", null);
            testTrade.Stock = stock;

            //Assert
            Assert.Equal(stock, testTrade.Stock);
        }
        [Fact]
        public void UserTest4()
        {
            //Arrange

            //Act
            testTrade.Id = 1;

            //Assert
            Assert.Equal(1, testTrade.Id);
        }

        public void Dispose()
        {
            testTrade = null;
        }
    }
}
