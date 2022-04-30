using AutoMapper;
using Climapi.Common.DTO.Request;
using Climapi.Common.DTO.Response;
using Climapi.Core.Entities;
using Climapi.Core.Entities.Enums;
using Climapi.Services.Exceptions.BadRequest;
using Climapi.Services.Exceptions.NotFound;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Climapi.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<UpdateUserDto> _updateValidator;

        public UserService(IMapper mapper,
            UserManager<AppUser> userManager,
            IValidator<RegisterDto> registerValidator,
            IValidator<UpdateUserDto> updateValidator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _registerValidator = registerValidator;
            _updateValidator = updateValidator;
        }

        public async Task Delete(string id)
        {
            AppUser user = await GetUser(id);
            var result = await _userManager.DeleteAsync(user);
            // If Delete failed throw Exception
            if (!result.Succeeded) throw new DbUpdateFailedBadRequestException(result.Errors.First().Description);
        }

        public async Task<UserDto> Get(string id)
        {
            AppUser user = await GetUser(id);
            
            return await MakeUserDto(user);
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                userDtos.Add(await MakeUserDto(user));
            }
            return userDtos;
        }

        public async Task HardUpdate(string id, RegisterDto newUser)
        {
            Validate(_registerValidator, newUser);

            // Find actual user with passed id and delete it
            AppUser current = await GetUser(id);
            await Delete(current.Id);

            // Create new user with same Id than previous
            AppUser user = _mapper.Map<AppUser>(newUser);
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
            Validate(_registerValidator, userDto);

            // Try to parse and add User
            AppUser user = _mapper.Map<AppUser>(userDto);
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
            
            return await MakeUserDto(user);
        }

        public async Task Put(string id, UpdateUserDto newData)
        {
            Validate(_updateValidator, newData);

            AppUser current = await GetUser(id);
            // Change atributes of current user for ones of newUser with auxiliar method
            UpdateUser(current, newData);

            var result = await _userManager.UpdateAsync(current);
            // If Update failed throw exception
            if (!result.Succeeded) throw new DbUpdateFailedBadRequestException(result.Errors.First().Description);
        }


        private async Task<AppUser> GetUser(string id)
        {
            // Search for the user with given id
            AppUser? user = await _userManager.FindByIdAsync(id);
            // If user does not exists, throw Exception
            if (user == null) throw new UserNotFoundException();
            return user;
        }

        private static void UpdateUser(AppUser actualUser, UpdateUserDto newData)
        {
            actualUser.Email = newData.Email;
            actualUser.UserName = newData.Username;
        }

        private static void Validate<T>(IValidator<T> validator, T toValidate)
        {
            var result = validator.Validate(toValidate);
            if (!result.IsValid)
                throw new InvalidFieldBadRequestException(result.Errors.First().ErrorMessage);
        }

        private async Task<UserDto> MakeUserDto(AppUser user)
        {
            UserDto dto = _mapper.Map<UserDto>(user);

            dto.Roles = (await _userManager.GetRolesAsync(user)).ToList();

            return dto;
        }
    }
}
