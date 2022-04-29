using AutoMapper;
using Climapi.Common.DTO.Request;
using Climapi.Common.DTO.Response;
using Climapi.Core.Entities;
using Climapi.Core.Entities.Enums;
using Climapi.Services.Exceptions.BadRequest;
using Climapi.Services.Exceptions.NotFound;
using Climapi.Services.Exceptions.Unauthorized;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Climapi.Services.Impl
{
    public class AuthManagerService : IAuthManagerService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<ChangePasswordDto> _chngPassValidator;

        public AuthManagerService(IConfiguration configuration,
            UserManager<User> userManager, IMapper mapper,
            IValidator<LoginDto> loginValidator, IValidator<RegisterDto> registerValidator,
            IValidator<ChangePasswordDto> chngPassValidator)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
            _chngPassValidator = chngPassValidator;
        }
        public async Task<string> AuthenticateAsync(LoginDto loginDto)
        {
            // Validate de Dto
            Validate(_loginValidator, loginDto);

            // Search for the user with email given and throw exception if not exists
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new UserNotFoundException("The User with such Mail doesn't exist", (int)CustomCodeEnum.UserNotFound);
            }

            // Check for Password from dto match user password and throw exception if not
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                throw new UnauthorizedException("The Password provided is wrong", (int)CustomCodeEnum.WrongPassword);
            }

            // Return de JWT Token
            return await CreateToken(user);
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            // Validate dto
            Validate(_registerValidator, registerDto);

            // Parse data to User object and try to Create it
            // Throw exception if error
            var user = _mapper.Map<User>(registerDto);
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                throw new InvalidFieldBadRequestException(result.Errors.First().Description, (int)CustomCodeEnum.FailedRegistration);

            // Add Role to Created User
            result = await _userManager.AddToRoleAsync(user, Enum.GetName(RoleEnum.User));
            if (!result.Succeeded)
                throw new InvalidFieldBadRequestException(result.Errors.First().Description, (int)CustomCodeEnum.FailedRoleAdded);

            // Return Dto with data of created user
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async Task ChangePassword(ChangePasswordDto chngPassDto, string id)
        {
            // Validate Dto
            Validate(_chngPassValidator, chngPassDto);

            // Get User with passed Id
            User user = await GetUser(id);

            // Attemp to change Password or throw Exception
            var result = await _userManager.ChangePasswordAsync(user, chngPassDto.Password, chngPassDto.NewPassword);
            if (!result.Succeeded)
                throw new UnauthorizedException(result.Errors.First().Description);
        }

        private async Task<string> CreateToken(User user)
        {
            var credentials = GetSigningCredentials();
            var claims = await GetClaims(user);

            return GetToken(credentials, claims);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("JwtKey")
                ?? _configuration.GetSection("Jwt").GetValue<string>("Key");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha512Signature);
        }
        private async Task<List<Claim>> GetClaims(User user)
        {
            var issuer = _configuration.GetSection("Jwt").GetValue<string>("Issuer");
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email, issuer),
                new Claim(ClaimTypes.AuthenticationMethod, "bearer", ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.NameIdentifier, user.UserName, ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.UserData, user.Id, ClaimValueTypes.String, issuer)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role, ClaimValueTypes.String, issuer));
            }

            return claims;
        }
        private string GetToken(SigningCredentials credentials, IEnumerable<Claim> claims, bool encrypt = false)
        {
            var issuer = _configuration.GetSection("Jwt")["Issuer"];

            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: _configuration.GetSection("Jwt")["Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration.GetSection("Jwt")["ExpireTime"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private static void Validate<T>(IValidator<T> validator, T toValidate)
        {
            var result = validator.Validate(toValidate);
            if (!result.IsValid)
                throw new InvalidFieldBadRequestException(result.Errors.First().ErrorMessage);
        }

        private async Task<User> GetUser(string id)
        {
            // Search for the user with given id
            User user = await _userManager.FindByIdAsync(id);
            // If user does not exists, throw Exception
            if (user == null) throw new UserNotFoundException();
            return user;
        }
    }
}
