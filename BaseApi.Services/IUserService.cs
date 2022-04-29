using Climapi.Common.DTO.Request;
using Climapi.Common.DTO.Response;

namespace Climapi.Services
{
    public interface IUserService
    {
        Task<UserDto> Get(string id);
        Task<List<UserDto>> GetAll();
        Task<UserDto> Post(RegisterDto user, string[]? roles = null);
        Task Put(string id, UpdateUserDto newData);
        Task HardUpdate(string id, RegisterDto user);
        Task Delete(string id);
    }
}
