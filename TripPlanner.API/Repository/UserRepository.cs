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

        public async Task<User> GetUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            var userFromContext = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            
            if (userFromContext != null)
            {
                userFromContext.FirstName = user.FirstName;
                userFromContext.LastName = user.LastName; 
                userFromContext.Email = user.Email;    
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddUserAsync(User user)
        {           
            await _context.Users.AddAsync(user);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteUserAsync(int id)
        {
            var userFromContext = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            _context.Users.Remove(userFromContext);
            return await _context.SaveChangesAsync();
            
        }


    }
}