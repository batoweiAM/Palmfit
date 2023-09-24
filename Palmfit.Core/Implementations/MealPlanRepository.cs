using Microsoft.EntityFrameworkCore;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.AppDbContext;
using Palmfit.Data.EntityEnums;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Palmfit.Core.Implementations
{
	public class MealPlanRepository : IMealPlanRepository
	{
		private readonly PalmfitDbContext _palmfitDbContext;
		
		public MealPlanRepository(PalmfitDbContext palmfitDbContext)
		{
			_palmfitDbContext = palmfitDbContext;		
		}





		public async Task<IEnumerable<IEnumerable<MealPlanDto>>> GetPaginatedWeeklyPlans(string foodClassId, int pageNumber)
		{
			try
			{
				var foodItemBasedOnCategory = await _palmfitDbContext.MealPlans
					.Where(x => x.FoodClassId == foodClassId)
					.Include(prop => prop.Food)
					.OrderBy(x => x.Day)  // Order by Day to group data
					.ToListAsync();

				if (!foodItemBasedOnCategory.Any())
				{
					return null;
				}

				var groupedPlans = foodItemBasedOnCategory
					.GroupBy(x => x.Day)  // Group by Day
					.Skip((pageNumber - 1) * 7)  // Skip groups for previous pages
					.Take(7)  // Take the next 7 days for pagination
					.Select(group => group.Select(item => new MealPlanDto
					{
						Day = item.Day,
						MealOfDay = Convert.ToString(item.MealOfDay),
						Name = item.Food.Name,
						Description = item.Food.Description,
						Details = item.Food.Details,
						Origin = item.Food.Origin,
						Image = item.Food.Image,
						Carbs = item.Food.Carbs,
						Proteins = item.Food.Proteins,
						Fats = item.Food.Fats,
						Calorie = item.Food.Calorie,
						Unit = Convert.ToString(item.Food.Unit)
					}));

				return groupedPlans;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}





		public async Task<IEnumerable<MealPlanDto>> GetWeeklyPlan(string foodClassId)
		{
			try
			{

				var foodItemBasedOnCategory = await _palmfitDbContext.MealPlans.Where(x => x.FoodClassId == foodClassId).Include(prop => prop.Food).ToListAsync();

				if (!foodItemBasedOnCategory.Any())
				{
					return null;
				}

				List<MealPlanDto> result = new();

				foreach (var item in foodItemBasedOnCategory)
				{
					var mealPlan = new MealPlanDto
					{
						Day = item.Day,
						MealOfDay = Convert.ToString(item.MealOfDay),
						Name = item.Food.Name,
						Description = item.Food.Description,
						Details = item.Food.Details,
						Origin = item.Food.Origin,
						Image = item.Food.Image,
						Carbs = item.Food.Carbs,
						Proteins = item.Food.Proteins,
						Fats = item.Food.Fats,
						Calorie = item.Food.Calorie,
						Unit = Convert.ToString(item.Food.Unit),



					};

					result.Add(mealPlan);
				}

				return result;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


		public async Task<string> AddMealPlan(PostMealDto postMealDto, string foodId, string foodClassId)
		{ 
            var MealToAdd = new MealPlan
            {
                Id = Guid.NewGuid().ToString(),
                MealOfDay = postMealDto.MealOfDay,
                FoodId = foodId,
				FoodClassId = foodClassId,
				Day = postMealDto.Day,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                UpdatedAt = DateTime.Now.ToUniversalTime(),
                IsDeleted = false,
            };

				await _palmfitDbContext.AddAsync(MealToAdd);
				_palmfitDbContext.SaveChanges();

				return "Food successfully added to Meal Plan!";

        }


		public async Task AddSelectedMealPlan(string appUserId,  string foodClassId)
		{	
			var selected = new SelectedPlans
			{
				Id = Guid.NewGuid().ToString(),
				FoodClassId = foodClassId,
				AppUserId = appUserId,
				CreatedAt = DateTime.UtcNow.ToUniversalTime(),
				UpdatedAt= DateTime.UtcNow.ToUniversalTime(),
				IsDeleted= false,
			};

			await _palmfitDbContext.SelectedPlans.AddAsync(selected);
			await _palmfitDbContext.SaveChangesAsync();
		
		}



		public async Task<string> UpdateSelectedMealPlan(string extistingfoodclassId, string newFoodClassId, string appUserId)
		{
			var existingMealPlans = await _palmfitDbContext.SelectedPlans.FirstOrDefaultAsync(row => row.FoodClassId == extistingfoodclassId && appUserId == row.AppUserId);

			  if(existingMealPlans == null)
			{
				return null;
			}

			existingMealPlans.IsDeleted = true;


			var newSelectedPlan = new SelectedPlans
			{
				Id = Guid.NewGuid().ToString(),
				FoodClassId = newFoodClassId,
				AppUserId = appUserId,
				CreatedAt = DateTime.Now.ToUniversalTime(),
				UpdatedAt = DateTime.Now.ToUniversalTime(),
				IsDeleted = false,
			};

			await _palmfitDbContext.AddAsync(newSelectedPlan);
			_palmfitDbContext.SaveChanges();

			return "Meal plan category successfully modified";
		}





		public async Task<IEnumerable<MealPlanDto>> GetSelectedPlan(string appUserId)
		{
			var getFoodId = await _palmfitDbContext.SelectedPlans.FirstOrDefaultAsync(x => x.AppUserId == appUserId);

			var mealPlan = await GetWeeklyPlan(getFoodId.FoodClassId);

			var selectedDate = getFoodId.CreatedAt.ToUniversalTime();
			var presentDay = (int)(DateTime.UtcNow - selectedDate).TotalDays;

			var groupedMeals = mealPlan
				.OrderBy(col => col.Day)
				.Where(x => x.Day >= presentDay && x.Day <= presentDay + 7)
				.GroupBy(x => x.Day);

			List<MealPlanDto> result = new List<MealPlanDto>();
			var dayOfWeek = DateTime.UtcNow.AddDays(-1).DayOfWeek; // Get the current day of the week
			var daysInWeek = Enum.GetValues(typeof(DayOfWeek)).Length;

			foreach (var group in groupedMeals)
			{

				dayOfWeek = (DayOfWeek)(((int)dayOfWeek + 1) % daysInWeek); // Increment dayOfWeek cyclically

				var orderedMeals = group.OrderBy(x =>
				{
					if (x.MealOfDay == "Breakfast")
						return 0;
					else if (x.MealOfDay == "Lunch")
						return 1;
					else
						return 2;
				}); // Order the meals within the group by breakfast, lunch, and dinner

				foreach (var item in orderedMeals)
				{
					var meal = new MealPlanDto
					{
						Day = item.Day,
						MealOfDay = item.MealOfDay,
						Name = item.Name,
						Description = item.Description,
						Details = item.Details,
						Origin = item.Origin,
						Image = item.Image,
						Calorie = item.Calorie,
						Unit = item.Unit,
						DayOfWeek = dayOfWeek.ToString(),
						Carbs = item.Carbs,
						Proteins = item.Proteins,
						Fats = item.Fats,
					};

					result.Add(meal);
				}
			}

			return result;
		}

		

		public async Task<bool> DeleteSelectedPlanAsync(string selectedplanId)
		{
			var selectedPlan = await _palmfitDbContext.SelectedPlans.FirstOrDefaultAsync(row => row.Id == selectedplanId);

			if (selectedPlan == null)
				return await Task.FromResult(false);

			_palmfitDbContext.Remove(selectedPlan);
			_palmfitDbContext.SaveChanges(true);

			return await Task.FromResult(true);
		}

	}
}
