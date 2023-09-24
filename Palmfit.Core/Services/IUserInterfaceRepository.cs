using Data.Entities;
using Palmfit.Core.Dtos;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Services
{
    public interface IUserInterfaceRepository
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<string> UpdateUserAsync(string id, UserDto userDto);
        Task<bool> DeleteUserAsync(string userId);
       
        Task<UserInfoDto> GetUserStatus(string id); 
        Task<UserDto> GetUserByIdAsync(string id);
    }
}
