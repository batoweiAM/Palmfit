using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.AppDbContext;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Implementations
{
    public class WalletRepository : IWalletRepository
    {

        private readonly PalmfitDbContext _palmfitDb;

        public WalletRepository(PalmfitDbContext palmfitDb)
        {
            _palmfitDb = palmfitDb;
        }

        public async Task<Wallet> GetWalletByUserId(string userId)
        {
            return await _palmfitDb.Wallets.FirstOrDefaultAsync(w => w.AppUserId == userId);
        }

        public async Task<Wallet> GetWalletByUserIdAsync(string appUserId)
        {
            var userWallet = await _palmfitDb.Wallets.FirstOrDefaultAsync(w => w.AppUserId == appUserId);
            if (userWallet == null)
            {
                return null;
            }
            return userWallet;
        }

        public async Task<string> RemoveFundFromWallet(string walletId, decimal amount)
        {
            var wallet = await _palmfitDb.Wallets.FirstOrDefaultAsync(w => w.Id == walletId);

            if (wallet == null)
            {
                return "Wallet not found"; // Wallet not found
            }

            if (wallet.Balance < amount)
            {
                return "Insufficient Balance, Please fund your wallet"; // Insufficient funds
            }

            wallet.Balance -= amount;
            return "Successfully removed fund"; // Successfully removed funds
        }

        public async Task<PaginParameter<WalletHistory>> WalletHistories(int page, int pageSize)
        {

            int totalCount = await _palmfitDb.WalletHistories.CountAsync();
            int skip = (page - 1) * pageSize;

            var histories = await _palmfitDb.WalletHistories
                .Include(i => i.Wallet)
                .OrderByDescending(t => t.Date)
                .Skip(skip)
                .ToListAsync();

            var paginatedResponse = new PaginParameter<WalletHistory>
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = histories
            };

            return paginatedResponse;
        }


        public async Task FundWalletAsync(FundWalletDto fundWalletDto, string userId)
        {
            var wallet = await _palmfitDb.Wallets.FirstOrDefaultAsync(x => x.AppUserId == userId);

            if (wallet == null)
            {
                // Create a new wallet if it doesn't exist
                wallet = new Wallet
                {
                    AppUserId = userId,
                    Balance = fundWalletDto.Amount

                };
                _palmfitDb.Wallets.Add(wallet);
            }
            else
            {
                // Update the existing wallet balance
                wallet.Balance += fundWalletDto.Amount;
            }

            await _palmfitDb.SaveChangesAsync();
        }

        public async Task<ICollection<TransactionDto>> GetUserTransactionHistory(string userId)
        {
            var getdata = await _palmfitDb.Transactions.Where(t => t.AppUserId == userId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
            if (!getdata.Any()) return null;

            List<TransactionDto> data = new();
            foreach (var transaction in getdata)
            {
                data.Add(new TransactionDto()
                {
                    Date = transaction.Date,
                    Description = transaction.Description,
                    Type = transaction.Type,
                    Channel = transaction.Channel,
                    Amount = transaction.Amount,
                    IsSuccessful = transaction.IsSuccessful,
                    Reference = transaction.Reference,
                    IpAddress = transaction.IpAddress,
                    Currency = transaction.Currency,
                    Vendor = transaction.Vendor,
                    AppUserId = transaction.AppUserId,
                });

            }
            return data;
        }

        public async Task<ICollection<WalletHistoryDto>> GetUserWalletHistory(string walletId)
        {
            var getData = await _palmfitDb.WalletHistories.Where(t => t.WalletAppUserId == walletId).OrderByDescending(t => t.Date).ToListAsync();
            if (!getData.Any()) return null;

            List<WalletHistoryDto> data = new();
            foreach (var item in getData)
            {
                data.Add(new WalletHistoryDto()
                {
                    Amount = item.Amount,
                    Type = item.Type,
                    Date = item.Date,
                    Reference = item.Reference,
                    Details = item.Details,
                    WalletAppUserId = item.WalletAppUserId,
                });
            }

            return data;
        }
        public async Task<List<Wallet>> GetAllWalletsAsync()
        {
            return await _palmfitDb.Wallets.ToListAsync();
        }
    }
}
