using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.Entities;

namespace Palmfit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferralController : ControllerBase
    {
        private readonly IReferralRepository _referralRepository;
        private readonly string _baseUrl;
        private readonly IAppUserRepository _appUserRepository;
        public ReferralController(IReferralRepository referralRepository, IConfiguration configuration, IAppUserRepository appUserRepository)
        {
            _referralRepository = referralRepository;
            _baseUrl = configuration.GetValue<string>("AppSettings:BaseUrl")!;
            _appUserRepository = appUserRepository;

        }

        [HttpGet("get-referral-link/{userId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetReferralLink(string userId)
        {
            string ReferralCode = _referralRepository.GenerateReferralCode(8);
            var user = _appUserRepository.GetUserById(userId);
            if (user == null)
            {
                return BadRequest(ApiResponse.Failed("User does not exist!"));
            }
            string referralLink = $"{_baseUrl}/referral/{ReferralCode}";
            return Ok(ApiResponse.Success(referralLink));
        }
    }
}
