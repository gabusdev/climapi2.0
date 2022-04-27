using Climapi.Common.DTO.Request;
using Climapi.Common.DTO.Response;
using System.Threading.Tasks;


namespace Climapi.Services
{
    public interface IAuthManagerService
    {
        Task<string> AuthenticateAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto loginDto);
    }
}
