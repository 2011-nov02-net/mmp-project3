using Domain.Models;
using System;
using Xunit;

namespace XUnitTest
{
    public class UserTest : IDisposable
    {
        User testUser;
        public UserTest()
        {
            testUser = new User("Paul", "Cortez", "pbcortez@revature.net", "pbcortez", null);

        }

        [Fact]
        public void UserTest1()
        {

            //Act
            testUser.FirstName = "Matthew";


            //Assert
            Assert.Equal("Matthew", testUser.FirstName);
        }

        [Fact]
        public void UserTest2()
        {
            //Arrange

            //Act
            testUser.LastName = "Goodman";

            //Assert
            Assert.Equal("Goodman", testUser.LastName);
        }
        [Fact]
        public void UserTest3()
        {
            //Arrange

            //Act
            testUser.Email = "mattg@revature.net";

            //Assert
            Assert.Equal("mattg@revature.net", testUser.Email);
        }
        [Fact]
        public void UserTest4()
        {
            //Arrange

            //Act
            testUser.UserName = "mattg";

            //Assert
            Assert.Equal("mattg", testUser.UserName);
        }
        [Fact]
        public void UserTest5()
        {
            //Arrange

            //Act
            testUser.Id = 1;

            //Assert
            Assert.Equal(1, testUser.Id);
        }
        public void Dispose()
        {
            testUser = null;
        }
    }
}
