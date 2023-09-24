using Palmfit.Data.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Data.Entities
{
	public class AllCalorieData : BaseEntity  
	{
		public WeightGoal WeightGoal { get; set; }
		public ActivityLevel ActivityLevel { get; set; }
		public int Age { get; set; }
		public decimal Height { get; set; }
		public decimal Weight { get; set; }
		public Gender Gender { get; set; }
		public string AppUserId { get; set; }
		public AppUser AppUser { get; set; }
	}
}
