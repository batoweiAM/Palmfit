using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Data.Entities
{
    public class Referral : BaseEntity
    {
        public string ReferralCode { get; set; }
        public string InvitedUserid { get; set; }
        public AppUser InvitedUser { get; set; }
    }
}
