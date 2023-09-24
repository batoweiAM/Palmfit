using Palmfit.Data.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Dtos
{
    public class WalletHistoryDto
    {
        public decimal Amount { get; set; }
        public WalletType Type { get; set; } 
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public string Details { get; set; }
        public string WalletAppUserId { get; set; }
    }
}
