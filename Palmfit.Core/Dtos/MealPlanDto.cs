using Palmfit.Data.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Dtos
{
	public class MealPlanDto
	{
		public int Day { get; set; }
		public string MealOfDay { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Details { get; set; }
		public string Origin { get; set; }
		public string Image { get; set; }
		public decimal Calorie { get; set; }
		public decimal Carbs { get; set; }
		public decimal Proteins { get; set; }
		public decimal Fats { get; set; }
		public string Unit { get; set; }
		public string? DayOfWeek { get; set; }
		

    }
}
