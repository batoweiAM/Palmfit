using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
//namespace Palmfit.Data.Entities
{
    public class UserOTP : BaseEntity
    //public class UserOTP:BaseEntity
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        //public int OTP { get; set; }
        public DateTime Expiration { get; set; }
    }
    

}
