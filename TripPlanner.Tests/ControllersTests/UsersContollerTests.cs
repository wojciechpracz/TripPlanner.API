using System.Collections.Generic;
using System.Linq;
using GenFu;
using Moq;
using TripPlanner.API.Models;
using TripPlanner.API.Repository;
using TripPlanner.API.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.API.Interfaces;

namespace TripPlanner.Tests.ControllersTests
{
    public class UsersContollerTests
    {
        [Fact]
        public async void GetUsers_ReturnsAllUsers()
        {
            //arange
            var repo = new Mock<IUserRepository>();
            var users = GetFakeUsers();
            repo.Setup(x => x.GetUsersAsync()).ReturnsAsync(users);
            var UsersController = new UsersController(repo.Object);

            //act
            var response = await UsersController.GetUsers();
            IEnumerable<User> result = response.Value;

            //asert
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public async void GetUser_ReturnsOkResponse()
        {
            //arange
            var repo = new Mock<IUserRepository>();
            var user = new User{Id = 23, FirstName = "Jasiek", LastName = "Kowalski"};
            repo.Setup(x => x.GetUserAsync(It.IsAny<int>())).ReturnsAsync(user);
            var UsersController = new UsersController(repo.Object);

            //act
            var response =  await UsersController.GetUser(23);
            
            //asert
            var result = Assert.IsType<OkObjectResult>(response.Result);
            var returnedValue = result.Value as User;
            Assert.Equal(23, returnedValue.Id);
            //Assert.Equal(23, result.Value);
        }
        


        private IEnumerable<User> GetFakeUsers()
        {
            var i = 1;
            var users = A.ListOf<User>(10);
            users.ForEach(u => u.Id = i++);
            return users.Select(_ => _);
        }
    }
}