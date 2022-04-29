using Climapi.Common.DTO.Request;
using Climapi.Common.DTO.Response;
using Climapi.Core.Entities;
using Climapi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Climapi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<UpdateUserDto> _updateValidator;

        public UserController(IUserService userService,
            IValidator<RegisterDto> registerValidator,
            IValidator<UpdateUserDto> updateValidator)
        {
            _userService = userService;
            _registerValidator = registerValidator;
            _updateValidator = updateValidator;
        }

        // GET: api/user
        [HttpGet]
        [Authorize(Policy = "AdminRights")]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            return Ok(await _userService.GetAll());
        }

        // GET api/user/5
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminRights")]
        public async Task<ActionResult<UserDto>> Get(string id)
        {
            return Ok(await _userService.Get(id));
        }
        
        // GET api/user/me
        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> Info()
        {
            string currentId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)!.Value;
            return Ok(await _userService.Get(currentId));
        }

        // POST api/user
        [HttpPost]
        [Authorize(Policy = "AdminRights")]
        public async Task<ActionResult<UserDto>> Post(RegisterDto newUser)
        {
            // Check for validity of newUser Dto
            var result = _registerValidator.Validate(newUser);
            if (!result.IsValid)
                return BadRequest(result);

            var user = await _userService.Post(newUser);
            return Created($"api/user/{user.Id}", user);
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminRights")]
        public async Task<ActionResult> SoftUpdate(UpdateUserDto newData, string id)
        {
            // Check for validity of newUser Dto
            var result = _updateValidator.Validate(newData);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            await _userService.Put(id, newData);
            return NoContent();
        }
        
        // PUT api/user
        [HttpPut()]
        public async Task<ActionResult> Put(UpdateUserDto newData)
        {
            // Check for validity of newUser Dto
            var result = _updateValidator.Validate(newData);
            if (!result.IsValid)
                return BadRequest(result);

            string id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)!.Value;
            await _userService.Put(id, newData);
            return NoContent();
        }

        // PUT api/user/update/5
        [HttpPut("update/{id}")]
        [Authorize(Policy = "AdminRights")]
        public async Task<ActionResult> HardUpdate(RegisterDto user, string id)
        {
            await _userService.HardUpdate(id, user);
            return NoContent();
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminRights")]
        public async Task<ActionResult> Delete(string id)
        {
            await _userService.Delete(id);
            return NoContent();
        }
        
    }
}
