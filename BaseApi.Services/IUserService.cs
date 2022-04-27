using Climapi.Common.DTO.Response;
using Climapi.Core.Entities;
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
        Task<UserDto> Post(User user);
        Task<bool> Put(User user);
        Task<bool> Delete(string id);
        

    }
}
