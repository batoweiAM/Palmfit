using Microsoft.EntityFrameworkCore;
using Palmfit.Core.Services;
using Palmfit.Data.AppDbContext;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Implementations
{
    public class ReferralRepository : IReferralRepository
    {
        private readonly PalmfitDbContext _dbContext;
        public ReferralRepository(PalmfitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Referral> GetReferralByUserId(string userId)
        {
            var referal = await _dbContext.Referrals.FirstOrDefaultAsync(r => r.InvitedUserid == userId);
            return referal ?? throw new Exception("Referral does not exist");
        }

        public async Task AddReferral(Referral referral)
        {
            _dbContext.Referrals.Add(referral);
            await _dbContext.SaveChangesAsync();
        }
        //code to generate referral code
        public string GenerateReferralCode(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var referralCode = new char[length];

            for (int i = 0; i < length; i++)
            {
                referralCode[i] = characters[random.Next(characters.Length)];
            }

            return new string(referralCode);
        }
    }
}
