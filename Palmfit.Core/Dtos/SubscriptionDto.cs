using Palmfit.Data.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Dtos
{
	public class SubscriptionDto
	{
		public SubscriptionType Type { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string AppUserId { get; set; }
		public bool IsExpired { get; set; }
		public string SubscriptionId { get; set; }

	}
}
