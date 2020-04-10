using System.Collections.Generic;
using System.Linq;
using GenFu;
using Moq;
using TripPlanner.API.Models;
using Xunit;
using Microsoft.EntityFrameworkCore;
using TripPlanner.API.Data;
using TripPlanner.API.Repository;


namespace TripPlanner.Tests.RepositoryTests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async void GetAllUsers_returns_all_users()
        {
            //arange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "GetAllUsers_returns_all_users")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DataContext(options))
            {
                context.Users.Add(new User {FirstName = "Jan", LastName = "Kowalski", Email = "jan@gmail.com"});
                context.Users.Add(new User {FirstName = "Henryk", LastName = "Kwiatkowski", Email = "Henryk@gmail.com"});
                context.Users.Add(new User {FirstName = "Mirosław", LastName = "Nowak", Email = "Mirosław@gmail.com"});
                context.SaveChanges();
            }

            
            //act
            using(var context = new DataContext(options)) 
            {
                var repo = new UserRepository(context);
                var result = await repo.GetUsersAsync();

                //asert
                Assert.Equal(3, result.Count());
            }
        }        
    }
}