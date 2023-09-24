using Palmfit.Core.Dtos;
using Palmfit.Core.Implementations;
using Palmfit.Data.Entities;
using Palmfit.Data.EntityEnums;
using Palmfit.Core.Dtos;
using Palmfit.Core.Implementations;
using Palmfit.Data.Entities;
using Palmfit.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Services
{
    public interface IFoodInterfaceRepository
    { 
   
        Task<List<Food>> GetAllFoodAsync();
        Task<List<FoodDto>> SearchFood(string searchTerms);
        Task<String> UpdateFoodClass(string foodClassId, FoodClassDto updatedFoodClassDto);

        Task<Food> GetFoodById(string id);
        /* < Start----- required methods to Calculate Calorie -----Start > */
        Task<CalorieDto> GetCalorieByNameAsync(string foodName, UnitType unit, decimal amount);
        Task<decimal> GetCalorieByIdAsync(string foodId, UnitType unit, decimal amount);
        Task<decimal> CalculateTotalCalorieAsync(Dictionary<string, (UnitType unit, decimal amount)> foodNameAmountMap);
        Task<IEnumerable<Food>> GetFoodsByNameAsync(string foodName);
        Task<IEnumerable<Food>> GetFoodsByIdAsync(string foodId);

        /* < End----- required methods to Calculate Calorie -----End > */
        Task<string> UpdateFoodAsync(string id, UpdateFoodDto foodDto);
        Task AddFoodAsync(Food food);
        Task AddFoodClassAsync(FoodClass foodClass);
        Task<ICollection<FoodDto>> GetFoodByCategory(string id);
		Task<FoodClass> GetFoodClassesByIdAsync(string foodClassId);
		string DeleteFoodClass(string foodClassId);
        Task<Food> GetFoodByIdAsync(string id);
        Task<string> DeleteAsync(string id);
        Task<string> CreateFoodClass(FoodClassDto foodClassDto);

    }
}