using Palmfit.Data.EntityEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Data.Entities
{
	public class MealPlan : BaseEntity
	{
        
        public FoodClass FoodClass { get; set; }
        public string FoodClassId { get; set; }
        public Food Food { get; set; }
        public string FoodId { get; set; }
        public int Day { get; set; }
        public MealOfDay MealOfDay { get; set; }

    }
}


