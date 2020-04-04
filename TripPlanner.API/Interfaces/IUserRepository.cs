using System.Collections.Generic;
using System.Threading.Tasks;
using TripPlanner.API.Models;

namespace TripPlanner.API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync(); 

    }
}