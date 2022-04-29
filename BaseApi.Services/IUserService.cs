using Climapi.Common.DTO.Request;
using Climapi.Common.DTO.Response;
using Climapi.Core.Entities;
using Climapi.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
