using Palmfit.Data.Entities;
using Palmfit.Data.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Dtos
{
    public class UserInfoDto
    {
        public DateTime? LastOnline { get; set; }
        public bool? IsVerified { get; set; }
        public bool? Active { get; set; }
        public string? ReferralCode { get; set; }
        public string? InviteCode { get; set; }
        public SubscriptionType Type { get; set; }
        public bool IsExpired { get; set; }
        public DateTime EndDate { get; set; }
    }
}
