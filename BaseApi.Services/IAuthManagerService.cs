using Climapi.Common.DTO.Request;
using Climapi.Common.DTO.Response;


namespace Climapi.Services
{
    public interface IAuthManagerService
    {
        Task<string> AuthenticateAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto loginDto);
        Task ChangePasswordAsync(ChangePasswordDto chngPassDto, string id);
        Task<string> GetResetPasswordTokenAsync(string email);
        Task ResetPasswordAsync(ResetPasswordDto newPass);
    }
}
