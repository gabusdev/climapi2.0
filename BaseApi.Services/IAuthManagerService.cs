using Climapi.Common.DTO.Request;
using Climapi.Common.DTO.Response;


namespace Climapi.Services
{
    public interface IAuthManagerService
    {
        Task<string> AuthenticateAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto loginDto);
        Task ChangePassword(ChangePasswordDto chngPassDto, string id);
    }
}
