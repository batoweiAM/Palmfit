using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.Entities;
using System.Security.Claims;

namespace Palmfit.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly UserManager<AppUser> _userManager;

        public SubscriptionController(ISubscriptionRepository subscriptionRepository, UserManager<AppUser> userManager)
        {
            _subscriptionRepository = subscriptionRepository;
            _userManager = userManager;
        }

        [HttpGet("get-subscription-by-id")]
        public async Task<ActionResult<ApiResponse<Subscription>>> GetSubscriptionById(string subscriptionId)
        {
            try
            {
                var subscription = await _subscriptionRepository.GetSubscriptionByIdAsync(subscriptionId);

                if (subscription == null)
                    return ApiResponse<Subscription>.Failed(null, "Subscription not found");

                return ApiResponse<Subscription>.Success(subscription, "Subscription found");
            }
            catch (Exception ex)
            {
                return ApiResponse<Subscription>.Failed(null, ex.Message);
            }
        }

        [HttpGet("get-subscriptions-by-user")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Subscription>>>> GetSubscriptionsByUser(string userId)
        {
            try
            {
                var subscriptions = await _subscriptionRepository.GetSubscriptionsByUserIdAsync(userId);

                if (!subscriptions.Any())
                    return ApiResponse<IEnumerable<Subscription>>.Failed(null, "Subscriptions not found");

                return ApiResponse<IEnumerable<Subscription>>.Success(subscriptions, "Subscriptions found");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<Subscription>>.Failed(null, ex.Message);
            }
        }

        [HttpGet("get-subscriptions-by-username")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Subscription>>>> GetSubscriptionsByUsername(string userName)
        {
            try
            {
                var subscriptions = await _subscriptionRepository.GetSubscriptionsByUserNameAsync(userName);

                if (!subscriptions.Any())
                    return ApiResponse<IEnumerable<Subscription>>.Failed(null, "Subscriptions not found");

                return ApiResponse<IEnumerable<Subscription>>.Success(subscriptions, "Subscriptions found");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<Subscription>>.Failed(null, ex.Message);
            }
        }


        [HttpPost("create-subscription")]
        public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionDto subscriptionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Failed("Invalid subscription data."));
            }
            var loggedInUser = User.FindFirst(ClaimTypes.NameIdentifier);

            // Ensuring the user exists
            var user = await _userManager.FindByIdAsync(loggedInUser.Value);
            if (user == null)
            {
                return NotFound(ApiResponse.Failed("User not found."));
            }

            var createdSubscription = await _subscriptionRepository.CreateSubscriptionAsync(subscriptionDto, HttpContext.User);
            if (createdSubscription != null)
            {
                return Ok(ApiResponse.Success("Subscription created successfully."));
            }
            else
            {
                return BadRequest(ApiResponse.Failed("Subscription creation failed."));
            }
        }

        [HttpDelete("/subscription")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteSubscription(string subscriptionId)
        {
            try
            {
                var result = await _subscriptionRepository.DeleteSubscriptionAsync(subscriptionId);

                if (!result)
                    return ApiResponse<bool>.Failed(false, "Subscription not found");

                return ApiResponse<bool>.Success(true, "Subscription deleted");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Failed(false, ex.Message);
            }
        }


        [HttpGet("subscription-status")]
        public async Task<IActionResult> GetSubscriptionStatus()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);
                var availableSubscription = await _subscriptionRepository.GetUserSubscriptionStatusAsync(userId);

                if (user == null)
                {
                    return NotFound(ApiResponse.Success("User not found."));
                }

                if (availableSubscription != null)
                {
                    return Ok(ApiResponse.Success(availableSubscription));
                }
                else
                {
                    return NotFound(ApiResponse.Success("User has no Subscription."));
                }
            }
            catch
            {
                return BadRequest(ApiResponse.Failed("Could not retrieve subscription status"));
            }
         }

		[HttpPut("update-subscription")]
		public async Task<IActionResult> UpdateSubscription([FromBody] SubscriptionDto subscriptionDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ApiResponse.Failed("Invalid subscription data."));
			}
			try
			{
				var message = await _subscriptionRepository.UpdateSubscriptionAsync(subscriptionDto);
				return Ok(ApiResponse.Success(message));
			}
			catch (Exception ex)
			{
				return BadRequest(ApiResponse.Failed("An error occurred while updating the subscription.", errors: new List<string> { ex.Message }));
			}
		}



	}
}

