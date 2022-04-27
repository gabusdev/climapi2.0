using AutoMapper;
using Climapi.Common.DTO.Response;
using Climapi.Core.Entities;
using DataEF.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climapi.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<bool> Delete(string id)
        {
            
            User user = await _userManager.FindByIdAsync(id);
            /*
            if (user is null)
            {
                return false;
            }
            */
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<UserDto> Get(string id)
        {
            return _mapper.Map<UserDto>(await _userManager.FindByIdAsync(id));
        }

        public async Task<List<UserDto>> GetAll()
        {
            return _mapper.Map<List<UserDto>>(await _userManager.Users.ToListAsync());
        }

        public async Task<UserDto> Post(User user)
        {
            await _userManager.CreateAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> Put(User newUser)
        {
            var result = await _userManager.UpdateAsync(newUser);
            return result.Succeeded;
        }
    }
}
