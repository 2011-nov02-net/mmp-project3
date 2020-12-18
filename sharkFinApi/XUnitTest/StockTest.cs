using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
    public class StockTest : IDisposable
    {
        Stock testStock;
        public StockTest()
        {
            testStock = new Stock
            {
                Symbol = "AAPL",
                Market = "Technology",
                Logo = "Apple",
                Name = "Apple Inc."
            };
        }

        [Fact]
        public void UserTest1()
        {

            //Act
            testStock.Symbol = "paul";


            //Assert
            Assert.Equal("paul", testStock.Symbol);
        }

        [Fact]
        public void UserTest2()
        {
            //Arrange

            //Act
            testStock.Market = "Human";

            //Assert
            Assert.Equal("Human", testStock.Market);
        }
        [Fact]
        public void UserTest3()
        {
            //Arrange

            //Act
            testStock.Logo = "face";

            //Assert
            Assert.Equal("face", testStock.Logo);
        }
        [Fact]
        public void UserTest4()
        {
            //Arrange

            //Act
            testStock.Name = "Cortez";

            //Assert
            Assert.Equal("Cortez", testStock.Name);
        }

        public void Dispose()
        {
            testStock = null;
        }
    }
}
