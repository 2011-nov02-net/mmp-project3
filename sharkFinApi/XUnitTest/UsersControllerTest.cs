using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using DataAccess;
using sharkFinApi.Controllers;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace XUnitTest
{
    public class UsersControllerTest
    {
        static readonly Mock<IUserRepository> _mockRepo = new Mock<IUserRepository>();
        static readonly UsersController usersController = new UsersController(_mockRepo.Object, null, new NullLogger<UsersController>());
        string fake = "fakeId";
        string fake2 = "fakeId2";
    }
    //[Fact]
    //public async Task UserController_GetAllUsers()
   // {
     
        
   // }
}
