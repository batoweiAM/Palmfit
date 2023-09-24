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
    public class WalletTransactionsController : ControllerBase
    {
        private readonly IWalletTransactionRepository _transaction;
        public WalletTransactionsController(IWalletTransactionRepository transaction)
        {
            _transaction = transaction;
        }

        [HttpGet("retrieve-wallet-histories")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllWalletTransactionHistory(int page, int pageSize)
        {
            var transactions = await _transaction.GetAllTransactionsAsync(page, pageSize);
            return Ok(ApiResponse.Success(transactions));
        }
    }
}
