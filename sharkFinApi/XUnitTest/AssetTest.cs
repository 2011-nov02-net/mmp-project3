using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
    public class AssetTest : IDisposable
    {
        Asset testAsset;
        public AssetTest()
        {
            var stock = new Stock("TSLA", "NASDAQ", "Tesla", null);
             testAsset = new Asset(stock, 10);
        }

        [Fact]
        public void UserTest1()
        {

            //Act
            testAsset.Quantity = 10;


            //Assert
            Assert.Equal(10, testAsset.Quantity);
        }

        [Fact]
        public void UserTest2()
        {
            //Arrange

            //Act
            testAsset.Id = 1;

            //Assert
            Assert.Equal(1, testAsset.Id);
        }
        [Fact]
        public void UserTest3()
        {
            //Arrange

            //Act
            var stock = new Stock("XXX", "NASDAQ", "XMAS", null);
            testAsset.Stock = stock;

            //Assert
            Assert.Equal(stock, testAsset.Stock);
        }
        public void Dispose()
        {
            testAsset = null;
        }
    }
}
