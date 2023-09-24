using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Services
{
    public interface IReferralRepository
    {
        Task<Referral> GetReferralByUserId(string userId);
        string GenerateReferralCode(int length);
        Task AddReferral(Referral referral);
    }
}
