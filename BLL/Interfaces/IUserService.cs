using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(User dtoUser);

        Task<ClaimsIdentity> AuthenticateAsync(User userDto);

        Task SetInitialDataAsync(User adminDto, List<string> roles);

        Task<User> GetUserAsync(string userId);

        Task DeleteUserAsync(string userId);

        Task AddRolesAsync(string userId, List<string> roles);

        Task<List<User>> GetUsersAsync();

        List<string> GetRoles();
    }
}
