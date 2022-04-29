using AutoMapper;
using Climapi.Common.DTO.Request;
using Climapi.Common.DTO.Response;
using Climapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace WebApi.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthManagerService _authManager;

        public AuthController(
            IMapper mapper,
            IAuthManagerService authManager)
        {
            _mapper = mapper;
            _authManager = authManager;
        }

        // POST api/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDTO)
        {
            Log.Information($"Registration Attemp for {registerDTO.Email}");

            var user = await _authManager.RegisterAsync(registerDTO);
            var userDto = _mapper.Map<UserDto>(user);

            return Created($"api/user/me", userDto);
        }

        // POST api/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDTO)
        {
            Log.Information($"Login Attemp for {loginDTO.Email}");

            var token = await _authManager.AuthenticateAsync(loginDTO);

            return Ok(token);
        }

        // POST api/passwordchange
        [HttpPost("passwordchange")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto chngPassDto)
        {
            var currentId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)!.Value;
            Log.Information($"Password Reset Attemp for {currentId}");

            await _authManager.ChangePasswordAsync(chngPassDto, currentId);

            return NoContent();
        }

        // POST api/forgotpassword
        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetResetPasswordToken([FromBody] string email)
        {
            Log.Information($"Reset Password Token Requested for User with mail {email}");
            var token = await _authManager.GetResetPasswordTokenAsync(email);

            return Ok(token);
        }

        [HttpPost("resetpassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPassDto)
        {
            Log.Information($"Reset Password Requested for User with mail {resetPassDto.Email}");
            await _authManager.ResetPasswordAsync(resetPassDto);

            return NoContent();
        }
    }
}
