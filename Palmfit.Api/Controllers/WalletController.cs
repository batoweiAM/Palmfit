using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Core.Implementations;
using Palmfit.Core.Services;
using Palmfit.Data.Entities;


namespace Palmfit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _wallet;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWalletRepository _walletRepository;
        public WalletController(IWalletRepository wallet, UserManager<AppUser> userManager)
        {
            _wallet = wallet;
            _userManager = userManager;
        }

        [HttpGet("appUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Wallet>> GetWallet(string appUserId)
        {
            try
            {
                if (string.IsNullOrEmpty(appUserId))
                {
                    return BadRequest(ApiResponse.Failed("Invalid user ID."));
                }

                var wallet = await _wallet.GetWalletByUserIdAsync(appUserId);

                if (wallet == null)
                {
                    return NotFound(ApiResponse.Failed("Wallet not found."));
                }

                return Ok(ApiResponse.Success(wallet));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Failed($"An error occurred: {ex.Message}"));

            }
        }

        [HttpPost("remove-funds")]
        public async Task<IActionResult> RemoveFunds(string walletId, decimal amount)
        {

            var removedFunds = await _wallet.RemoveFundFromWallet(walletId, amount);

            if(!removedFunds.Any())
            {
                return BadRequest(ApiResponse.Failed("Failed to perform transaction"));
            }
            return Ok(ApiResponse.Success("Funds successfully removed"));
            
        }


        [HttpGet("get-all-wallet-histories")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllWalletHistories(int page, int pageSize)
        {
            var paginatedResponse = await _wallet.WalletHistories(page, pageSize);
            if(paginatedResponse.TotalCount <= 0)
            {
                return BadRequest(ApiResponse.Failed("No wallet history found!"));
            }
            return Ok(ApiResponse.Success(paginatedResponse));
        }

        [HttpPost("fund-wallet")]
        public async Task<IActionResult> FundWallet([FromBody] FundWalletDto fundWalletDto, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(ApiResponse.Failed("User ID is required."));
            }

            // Check if the user exists
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {

                return BadRequest(ApiResponse.Failed("User not logged in or does not exist."));
            }

            try
            {
                await _wallet.FundWalletAsync(fundWalletDto, userId);
                return Ok(ApiResponse.Success("Wallet funded successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Failed(null, "Failed to fund wallet.", new List<string> { ex.Message }));
            }
        }

        [HttpGet("Get-User-Transaction-History")]
        public async Task<IActionResult> UserTransaction(string UserId)
        {
            var result = await _wallet.GetUserTransactionHistory(UserId);

            if (result == null) return NotFound(ApiResponse.Failed("No transaction performed"));

            return Ok(ApiResponse.Success(result));
        }

        [HttpGet("Get-User-Wallet-History")]
        public async Task<IActionResult> UserWallet(string walletId)
        {
            var result = await _wallet.GetUserWalletHistory(walletId);

            if (result == null) return NotFound(ApiResponse.Failed("No transaction performed"));

            return Ok(ApiResponse.Success(result));
        }

        [HttpGet("get-all-wallets")]
        public async Task<IActionResult> GetAllWallets()
        {
            try
            {
                var wallets = await _walletRepository.GetAllWalletsAsync();
                if (wallets.Any())
                {
                   return Ok(ApiResponse.Success(wallets));
                }
                  return NotFound(ApiResponse.Success("No wallets found."));

            }
            catch (Exception ex)
            {
                return StatusCode(500,ApiResponse.Failed("An error occured while processing the request."));
            }
        }
    }
}
