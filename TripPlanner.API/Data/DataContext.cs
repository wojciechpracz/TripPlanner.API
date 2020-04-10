using Microsoft.EntityFrameworkCore;
using TripPlanner.API.Models;

namespace TripPlanner.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DataContext() {}
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TodoItem> TodoItems { get; set; }

    }
}