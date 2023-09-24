using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Helpers
{
    public class RandomNumberGenerator
    {
        public int GenerateOTP()
        {
            // Generate a 6-digit OTP (you can adjust the length as needed)
            Random random = new Random();
            int otpValue = random.Next(100000, 999999);
            return otpValue;
        }
    }
}
