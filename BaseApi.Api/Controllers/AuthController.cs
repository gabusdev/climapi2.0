using AutoMapper;
using Climapi.Common.DTO.Request;
using Climapi.Common.DTO.Response;
using Climapi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace WebApi.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthManagerService _authManager;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IValidator<RegisterDto> _registerValidator;

        public AuthController(
            IMapper mapper,
            IAuthManagerService authManager,
            IValidator<LoginDto> loginValidator,
            IValidator<RegisterDto> registerValidator)
        {
            _mapper = mapper;
            _authManager = authManager;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] RegisterDto registerDTO)
        {
            Log.Information($"Registration Attemp for {registerDTO.Email}");
            var result = _registerValidator.Validate(registerDTO);
            if (!result.IsValid)
                return Results.BadRequest(result);

            var user = await _authManager.RegisterAsync(registerDTO);
            var userDto = _mapper.Map<UserDto>(user);
            return Results.Created($"user/{userDto.UserName}", userDto);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] LoginDto loginDTO)
        {
            Log.Information($"Login Attemp for {loginDTO.Email}");
            var result = _loginValidator.Validate(loginDTO);
            if (!result.IsValid)
                return Results.BadRequest(result);

            var token = await _authManager.AuthenticateAsync(loginDTO);

            return Results.Created($"user/",new { Token = token });
        }
    }
}
