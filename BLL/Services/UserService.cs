using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Dto;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Domain;
using DAL.Identity;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    internal class UserService : IUserService
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;
        private readonly IClientRepository _clientRepository;

        public UserService(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IClientRepository clientRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _clientRepository = clientRepository;
        }

        public async Task CreateAsync(User dtoUser)
        {
            var user = await _userManager.FindByNameAsync(dtoUser.UserName);
            if (user == null)
            {
                user = new ApplicationUser { Email = dtoUser.Email, UserName = dtoUser.UserName };
                var result = await _userManager.CreateAsync(user, dtoUser.Password);
                if (!result.Succeeded)
                {
                    throw new UserCreateException();
                }

                foreach (var roleName in dtoUser.Roles)
                {
                    await _userManager.AddToRoleAsync(user.Id, roleName);
                }

                var clientProfile = new ClientProfile
                {
                    Id = user.Id,
                    Address = dtoUser.Address,
                    Name = dtoUser.Name,
                };
                _clientRepository.Create(clientProfile);
            }
            else
            {
                throw new UserExistException();
            }
        }

        public async Task<ClaimsIdentity> AuthenticateAsync(User userDto)
        {
            ClaimsIdentity claim = null;
            var user = await _userManager.FindAsync(userDto.UserName, userDto.Password);
            if (user != null)
            {
                claim = await _userManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                if (claim.IsAuthenticated)
                {
                    return claim;
                }
            }

            throw new AuthenticateException();
        }

        public async Task SetInitialDataAsync(User adminDto, List<string> roles)
        {
            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await _roleManager.CreateAsync(role);
                }
            }

            await CreateAsync(adminDto);
        }

        public async Task<User> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotExistException();
            }

            var client = _clientRepository.GetClient(userId);

            return new User
            {
                Email = user.Email,
                Name = client.Name,
                Address = client.Address,
                UserName = user.UserName,
            };
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotExistException();
            }

            await _userManager.DeleteAsync(user);
        }

        public async Task AddRolesAsync(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotExistException();
            }

            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    throw new RoleExistException();
                }
            }

            await _userManager.RemoveFromRolesAsync(user.Id,
                _userManager.GetRoles(user.Id).ToArray());

            foreach (var roleName in roles)
            {
                await _userManager.AddToRoleAsync(user.Id, roleName);
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var userList = new List<User>();
            foreach (var item in _userManager.Users.ToList())
            {
                var roles = await _userManager.GetRolesAsync(item.Id);
                userList.Add(new User
                {
                    Id = item.Id,
                    Email = item.Email,
                    Roles = roles.ToList(),
                    UserName = item.UserName,
                    Name = _clientRepository.GetClient(item.Id).Name,
                });
            }

            return userList;
        }

        public List<string> GetRoles()
        {
            var roles = _roleManager.Roles.Select(role => role.Name).ToList();
            return roles;
        }
    }
}
