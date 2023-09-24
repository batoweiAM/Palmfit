using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Services
{
	public interface IMealPlanRepository
	{
		Task<IEnumerable<MealPlanDto>> GetWeeklyPlan(string foodClassId);
		Task<string> AddMealPlan([FromBody] PostMealDto postMealDto, string foodId, string foodClassId);
		Task AddSelectedMealPlan(string appUserId, string mealPlanId);
		Task<string> UpdateSelectedMealPlan(string extistingfoodclassId, string newFoodClassId, string appUserId);
		Task<bool> DeleteSelectedPlanAsync(string selectedplanId);

		Task<IEnumerable<MealPlanDto>> GetSelectedPlan(string appUserId);

		Task<IEnumerable<IEnumerable<MealPlanDto>>> GetPaginatedWeeklyPlans(string foodClassId, int pageNumber);
	}
}
