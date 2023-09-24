using Core.Helpers;
using Palmfit.Core.Dtos;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Services
{
    public interface IWalletRepository
    {
        Task<Wallet> GetWalletByUserId(string userId);
        Task<Wallet> GetWalletByUserIdAsync(string appUserId);
        Task<string> RemoveFundFromWallet(string walletId, decimal amount);
        Task<PaginParameter<WalletHistory>> WalletHistories(int page, int pageSize);
        Task FundWalletAsync(FundWalletDto fundWalletDto, string userId);
        Task<ICollection<TransactionDto>> GetUserTransactionHistory(string userId);
        Task<ICollection<WalletHistoryDto>> GetUserWalletHistory(string walletId);
        Task <List<Wallet>> GetAllWalletsAsync();
    }
}
