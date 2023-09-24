using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;


namespace Palmfit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InviteController : ControllerBase
    {
        private readonly IInviteRepository _inviteRepository;
       
        //ctor
        public InviteController(IInviteRepository inviteServices)
        {   
            _inviteRepository = inviteServices;
        }

        [HttpGet("{userId}/api-to-invites-of-user")]
        public async Task<IActionResult> GetInvitesByUserId(string userId)
        {
            try
            {
                var inviteDTOs = await _inviteRepository.GetInvitesByUserId(userId);
                return Ok(ApiResponse.Success(inviteDTOs));
            }
            catch (Exception)
            {
                return NotFound(ApiResponse.Failed("Invite does not exist."));
            }
        }


        [HttpDelete("Delete-Invite/{id}")]
        public async Task<IActionResult> DeleteInvite(string id)
        {

            var result = await _inviteRepository.Deleteinvite(id);

            if (!result)
                return NotFound(ApiResponse.Failed(result, "Invite not found"));

            return Ok(ApiResponse.Success(result));
        }


        [HttpGet("retrieve-all-user-invites")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUserInvites(int page, int pageSize)
        {
            var userInvites = await _inviteRepository.GetAllUserInvitesAsync(page, pageSize);
            if (userInvites.Data.Any())
            {
                return Ok(ApiResponse.Success(userInvites));
            }
            return NotFound(ApiResponse.Failed("No User Invite exists"));
        }
    }
}
