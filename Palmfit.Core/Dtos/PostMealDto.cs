using Palmfit.Data.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Dtos
{
    public class PostMealDto
    {
        public MealOfDay MealOfDay { get; set; }
		public int Day { get; set; }
	}
}
