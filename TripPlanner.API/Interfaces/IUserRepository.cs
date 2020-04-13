using System.Collections.Generic;
using System.Threading.Tasks;
using TripPlanner.API.Models;

namespace TripPlanner.API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync(); 
        Task<User> GetUserAsync(int id);
        Task<int> UpdateUserAsync(User user);
        Task<int> AddUserAsync(User user);
        Task<int> DeleteUserAsync(int id);
    }
}