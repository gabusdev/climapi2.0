using AutoMapper;
using Climapi.Common.DTO.Request;
using Climapi.Common.DTO.Response;
using Climapi.Core.Entities;
using Climapi.Core.Entities.Enums;
using Climapi.Services.Exceptions.BadRequest;
using Climapi.Services.Exceptions.BaseExceptions;
using Climapi.Services.Exceptions.NotFound;
using DataEF.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climapi.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        
        public async Task Delete(string id)
        {
            User user = await GetUser(id);
            var result = await _userManager.DeleteAsync(user);
            // If Delete failed throw Exception
            if (!result.Succeeded) throw new DbUpdateFailedBadRequestException(result.Errors.First().Description);
        }

        public async Task<UserDto> Get(string id)
        {
            User user = await GetUser(id);
            UserDto dto = _mapper.Map<UserDto>(user);
            dto.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            return dto;
        }

        public async Task<List<UserDto>> GetAll()
        {
            return _mapper.Map<List<UserDto>>(await _userManager.Users.ToListAsync());
        }

        public async Task HardUpdate(string id, RegisterDto newUser)
        {
            // Find actual user with passed id and delete it
            User current = await GetUser(id);
            await Delete(current.Id);

            // Create new user with same Id than previous
            User user = _mapper.Map<User>(newUser);
            user.Id = current.Id;

            // Add User
            var result = await _userManager.CreateAsync(user, newUser.Password);
            if (!result.Succeeded)
                throw new InvalidFieldBadRequestException(result.Errors.First().Description, (int)CustomCodeEnum.FailedRegistration);

            // Add Role for User
            result = await _userManager.AddToRoleAsync(user, Enum.GetName(RoleEnum.User));
            if (!result.Succeeded)
                throw new InvalidFieldBadRequestException(result.Errors.First().Description, (int)CustomCodeEnum.FailedRoleAdded);
        }

        public async Task<UserDto> Post(RegisterDto userDto, string[]? roles = null)
        {
            // Try to parse and add User
            User user = _mapper.Map<User>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            // If Create failed throw exception
            if (!result.Succeeded) throw new DbUpdateFailedBadRequestException(result.Errors.First().Description);

            // If param roles is null asign role User to the array of roles
            if (roles == null) roles = new string[] { Enum.GetName(RoleEnum.User)! };
            
            // Add Roles to new user
            foreach (var role in roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            var created = _mapper.Map<UserDto>(user);
            return created;
        }

        public async Task Put(string id, UpdateUserDto newData)
        {
            User current = await GetUser(id);
            // Change atributes of current user for ones of newUser with auxiliar method
            UpdateUser(current, newData);

            var result = await _userManager.UpdateAsync(current);
            // If Update failed throw exception
            if (!result.Succeeded) throw new DbUpdateFailedBadRequestException(result.Errors.First().Description);
        }

        
        private async Task<User> GetUser(string id)
        {
            // Search for the user with given id
            User user = await _userManager.FindByIdAsync(id);
            // If user does not exists, throw Exception
            if (user == null) throw new UserNotFoundException();
            return user;
        }
        
        private static void UpdateUser(User actualUser, UpdateUserDto newData)
        {
            actualUser.Email = newData.Email;
            actualUser.UserName = newData.Username;
        }

    }
}
