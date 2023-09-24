using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.Entities;

namespace Palmfit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterfaceRepository _user;

        public UserController(IUserInterfaceRepository userInterfaceRepository)
        {
            _user = userInterfaceRepository;
        }
        [HttpGet("get-all-User")]

        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
         
            var usersDto = await _user.GetAllUsersAsync();

            if (usersDto.Count() <= 0)
            {
                var res = await _user.GetAllUsersAsync();
                return NotFound(ApiResponse.Failed(res));
            }
            else
            { 
                var result = await _user.GetAllUsersAsync();

                return Ok(ApiResponse.Success(result));
            }
        }


        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, UserDto userDto)
        {
            var updateUser = await _user.UpdateUserAsync(id, userDto);
            if (updateUser == "User not found.")
                return NotFound(ApiResponse.Failed(updateUser));
            else if (updateUser == "User failed to update.")
            {
                return BadRequest(ApiResponse.Failed(updateUser));
            }

            return Ok(ApiResponse.Success(updateUser));

        }

        [HttpGet("{id}/get-a-user")]
        public async Task<ActionResult<UserDto>> GetUserById(string id)
        {
            UserDto userDto = await _user.GetUserByIdAsync(id);
            if (userDto == null)
            {
                return NotFound(ApiResponse.Failed("User does not exist."));
            }

            return Ok(ApiResponse.Success(userDto));
        }

        [HttpGet("Get-User-status/{id}")]
        public async Task<IActionResult> GetUserStatus(string id)
        {
            if (id == null) return BadRequest(ApiResponse.Failed(id, "Invalid Id"));

            var result = await _user.GetUserStatus(id);

            if(result == null) return NotFound(ApiResponse.Failed(result));

            return Ok(ApiResponse.Success(result));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var deletionResult = await _user.DeleteUserAsync(userId);

            if (deletionResult)
            {
                return Ok(new ApiResponse<string>("User deleted successfully."));
            }
            else
            {
                return BadRequest(new ApiResponse<string>("User deletion failed."));
            }
        }

    }
}
