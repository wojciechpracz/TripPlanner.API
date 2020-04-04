using Microsoft.EntityFrameworkCore;
using TripPlanner.API.Models;

namespace TripPlanner.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

    }
}