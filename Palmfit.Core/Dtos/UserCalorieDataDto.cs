using Palmfit.Data.EntityEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Dtos
{
	public class UserCalorieDataDto
	{
		[Required]
		public WeightGoal WeightGoal { get; set; }

		[Required]
		public ActivityLevel ActivityLevel { get; set; }

		[Required]
		public int Age { get; set; }

		[Required]
		public decimal Height { get; set; }

		[Required]
		public decimal Weight { get; set; }

		[Required]
		public Gender Gender { get; set; }

        [Required]
        public string AppUserId { get; set; }
    }
}
