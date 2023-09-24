using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Data.Entities
{
	public class SelectedPlans : BaseEntity
	{
		public FoodClass FoodClass { get; set; }
		public string FoodClassId { get; set; }
		public string AppUserId { get; set; }
		public AppUser AppUser { get; set; }
		
	}
}
