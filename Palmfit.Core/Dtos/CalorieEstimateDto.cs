using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Dtos
{
    public class CalorieEstimateDto
    {
        public decimal Inactive { get; set; }
        public decimal SomewhatActive { get; set; }
        public decimal Active { get; set; }
    }
}
