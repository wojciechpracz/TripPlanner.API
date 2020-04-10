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

        [Fact]
        public async void GetUserAsync_returns_correct_user()
        {
            //arange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "GetUsersAsync_returns_correct_users")
                .Options;

            using(var context = new DataContext(options))
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
                var result = await repo.GetUserAsync(2);

                //asert
                Assert.Equal("Henryk", result.FirstName);
                Assert.Equal("Kwiatkowski", result.LastName);
                Assert.Equal("Henryk@gmail.com", result.Email);
            }
        }

        [Fact]
        public async void UpdateUserAsync_updates_user()
        {
            //arange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "UpdateUserAsync_updates_user")
                .Options;

            using(var context = new DataContext(options))
            {
                context.Users.Add(new User {FirstName = "Jan", LastName = "Kowalski", Email = "jan@gmail.com"});
                context.Users.Add(new User {FirstName = "Henryk", LastName = "Kwiatkowski", Email = "Henryk@gmail.com"});
                context.Users.Add(new User {FirstName = "Mirosław", LastName = "Nowak", Email = "Mirosław@gmail.com"});                 
                context.SaveChanges();
             
            }

            using(var context = new DataContext(options)) 
            {
                var repo = new UserRepository(context);
                var user = new User { Id = 2, FirstName = "Krzysztof", LastName = "Kwiatkowski", Email="Henryk@gmail.com"};

                //act
                var result = await repo.UpdateUserAsync(user);

                //asert
                Assert.Equal(1, result);
            }
       }

       [Fact]
       public async void AddUserAsync_adds_user()
       {
            //arange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "AddUserAsync_adds_user")
                .Options;

            using(var context = new DataContext(options))
            {
                var repo = new UserRepository(context);
                var user = new User { Id = 2, FirstName = "Krzysztof", LastName = "Kwiatkowski", Email="Henryk@gmail.com"};

                //act
                var result = await repo.AddUserAsync(user);

                //asseert
                Assert.Equal(1, result);
            }
       }

       [Fact]
       public async void DeleteUserAsync_deletesUser()
       {
            //arange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "DeleteUserAsync_deletesUser")
                .Options;

            using(var context = new DataContext(options))
            {
                context.Users.Add(new User {FirstName = "Jan", LastName = "Kowalski", Email = "jan@gmail.com"});
                context.Users.Add(new User {FirstName = "Henryk", LastName = "Kwiatkowski", Email = "Henryk@gmail.com"});
                context.Users.Add(new User {FirstName = "Mirosław", LastName = "Nowak", Email = "Mirosław@gmail.com"});                 
                context.SaveChanges();     
            }

            using(var context = new DataContext(options))
            {
                var userId = 2;
                var repo = new UserRepository(context);

                //act
                repo.DeleteUserAsync(userId);

                //assert
                Assert.Equal(2, context.Users.Count());
            }

       }


    }
}