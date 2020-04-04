using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TripPlanner.API.Data;
using TripPlanner.API.Interfaces;
using TripPlanner.API.Models;

namespace TripPlanner.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            this._context = context;

        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            
            return users;
        }
    }
}