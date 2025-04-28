using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.DTOs.Request;
using ToDoApi.DTOs.Response;
using ToDoApi.Models;

namespace ToDoApi.Services.Interfaces
{
    public class UserService : IUserServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName,
            };
            var result = await userManager.CreateAsync(user,dto.Password);
            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
            }
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var u = await userManager.FindByIdAsync(id);
            if (u == null) return false;
            var result = await userManager.DeleteAsync(u);
            return result.Succeeded;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {

            return await userManager.Users
        .Select(u => new UserDto
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            FullName = u.FullName
        })
        .ToListAsync();
        }

        public async Task<UserDto?> GetByIdAsync(string id)
        {
            var u = await userManager.FindByIdAsync(id);
            if (u == null) return null;
            return new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                FullName = u.FullName
            };
        }

        public async Task<UserDto?> UpdateAsync(string id, UpdateUserDto dto)
        {
            var u = await userManager.FindByIdAsync(id);
            if (u == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                u.Email = dto.Email;
            if (dto.FullName != null)
                u.FullName = dto.FullName;

            var result = await userManager.UpdateAsync(u);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));

            return new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                FullName = u.FullName
            };
        }
    }
}
