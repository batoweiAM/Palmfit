//using Palmfit.Core.Dtos;
//using Palmfit.Data.Entities;
//using Palmfit.Core.Services;
//using Palmfit.Data.AppDbContext;
//using Palmfit.Data.EntityEnums;
//using Microsoft.EntityFrameworkCore;

//namespace Palmfit.Core.Implementations
//{
//    public class FoodInterfaceRepository : IFoodInterfaceRepository
//    {

//        private readonly PalmfitDbContext _dbContext;

//        public FoodInterfaceRepository(PalmfitDbContext dbcontext)
//        {
//            _dbContext = dbcontext;
//        }

//        public async Task<List<Food>> GetAllFoodAsync()
//        {
//            return await _dbContext.Foods.ToListAsync();
//        }

//        public async Task<Food> GetFoodById(string id)
//        {

//            var food = await _dbContext.Foods.FirstOrDefaultAsync(f => f.Id == id);

//            if (food == null)
//            {
//                return null;
//            }

//            return food;
//        }

//        public async Task<List<FoodDto>> SearchFood(string searchTerms)
//        {
//            try
//            {
//                var foodgs = new List<FoodDto>();
//                if (searchTerms != null)
//                {
//                    var foods = await _dbContext.Foods.Include(x => x.FoodClass).OrderByDescending(x => x.CreatedAt).ToListAsync();
//                    foods = foods.Where(x => x.Name.ToLower().Contains(searchTerms.ToLower().Trim())
//                    || x.FoodClass.Name.ToLower().Contains(searchTerms.ToLower().Trim())).ToList();

//                    var results = foods.Select(x => new FoodDto
//                    {
//                        Description = x.Description,
//                        Details = x.Details,
//                        Name = x.Name,
//                        Calorie = x.Calorie,
//                        Image = x.Image,
//                        Origin = x.Origin,
//                        Unit = x.Unit,
//                        FoodClass = x.FoodClass.Name,
//                    }).ToList();

//                    return results;
//                }
//                return foodgs;

//            }
//            catch (Exception ex)
//            {

//                throw;
//            }


//        }

//        public async Task AddFoodAsync(Food food)
//        {
//            // Generate a new GUID for the Food entity
//            food.Id = Guid.NewGuid().ToString();

//            // Add the new food to the database
//            await _dbContext.Foods.AddAsync(food);
//            await _dbContext.SaveChangesAsync();
//        }
//        public async Task<String> UpdateFoodClass(string foodClassId, FoodClassDto updatedFoodClassDto)
//        {
//            try
//            {
//                var foodClassEntity = await _dbContext.FoodClasses.FindAsync(foodClassId);
//                if (foodClassEntity == null)
//                {
//                    return "Food with ID does not exist";
//                }
//                foodClassEntity.Name = updatedFoodClassDto.Name;
//                foodClassEntity.Description = updatedFoodClassDto.Description;
//                foodClassEntity.Details = updatedFoodClassDto.Details;

//                await _dbContext.SaveChangesAsync();
//                var updatedFoodClassDtoResponse = new FoodClassDto
//                {
//                    Name = foodClassEntity.Name,
//                    Description = foodClassEntity.Description,
//                    Details = foodClassEntity.Details,
//                };
//                return "Food class updated successfully.";
//            }
//            catch (Exception ex)
//            {
//                return "Failed to update food class.";
//            }
//        }

//        /* < Start----- required methods to Calculate Calorie -----Start > */

//        private decimal ConvertToGrams(decimal amount, UnitType unit)
//        {
//            switch (unit)
//            {
//                case UnitType.Tablespoon:
//                    return amount * 14.3m;
//                case UnitType.Ounce:
//                    return amount * 28.4m;
//                case UnitType.Cup:
//                    return amount * 240m;
//                case UnitType.Piece:
//                    return amount * 1m;
//                default:
//                    throw new ArgumentException("Invalid unit type.", nameof(unit));
//            }
//        }

//        public async Task<CalorieDto> GetCalorieByNameAsync(string foodName, UnitType unit, decimal amount)
//        {
//            var food = await _dbContext.Foods.FirstOrDefaultAsync(f => f.Name.ToUpper() == foodName.ToUpper());
//            if (food == null)
//                throw new ArgumentException("Food not found with the specified name.", nameof(foodName));

//            decimal convertedAmount = ConvertToGrams(amount, unit);

//            decimal calorieFromFats = convertedAmount * food.Fats;
//            decimal calorieFromCarbs = convertedAmount * food.Carbs;
//            decimal calorieFromProteins = convertedAmount * food.Proteins;

//            decimal totalCalories = calorieFromFats + calorieFromCarbs + calorieFromProteins;

//            var calorieDto = new CalorieDto
//            {
//                Calorie = Math.Round(totalCalories),
//                Fats = Math.Round(amount * food.Fats, 2),
//                Carbs = Math.Round(amount * food.Carbs, 2),
//                Proteins = Math.Round(amount * food.Proteins, 2)
//            };

//            return calorieDto;
//        }




//        public async Task<decimal> GetCalorieByIdAsync(string foodId, UnitType unit, decimal amount)
//        {
//            var food = await _dbContext.Foods.FirstOrDefaultAsync(f => f.Id == foodId);
//            if (food == null)
//                throw new ArgumentException("Food not found with the specified ID.", nameof(foodId));

//            decimal convertedAmount = ConvertToGrams(amount, unit);
//            return food.Calorie * convertedAmount;
//        }

//        public async Task<decimal> CalculateTotalCalorieAsync(Dictionary<string, (UnitType unit, decimal amount)> foodNameAmountMap)
//        {
//            decimal totalCalorie = 0;

//            foreach (var kvp in foodNameAmountMap)
//            {
//                var food = await _dbContext.Foods.FirstOrDefaultAsync(f => f.Name == kvp.Key);
//                if (food == null)
//                    throw new ArgumentException($"Food not found with the specified Name: {kvp.Key}", nameof(foodNameAmountMap));

//                decimal convertedAmount = ConvertToGrams(kvp.Value.amount, kvp.Value.unit);
//                totalCalorie += food.Calorie * convertedAmount;
//            }

//            return totalCalorie;
//        }

//        /* < End----- required methods to Calculate Calorie -----End > */
//        public async Task<IEnumerable<Food>> GetFoodsByNameAsync(string foodName)
//        {
//            return await _dbContext.Foods.Where(f => f.Name == foodName).ToListAsync();
//        }

//        public async Task<IEnumerable<Food>> GetFoodsByIdAsync(string foodId)
//        {
//            return await _dbContext.Foods.Where(f => f.Id == foodId).ToListAsync();
//        }



//        public async Task<string> UpdateFoodAsync(string id, UpdateFoodDto foodDto)
//        {
//            var food = await _dbContext.Foods.FindAsync(id);

//            if (food == null)
//                return "Food not found.";

//            food.Name = foodDto.Name;
//            food.Description = foodDto.Description;
//            food.Details = foodDto.Details;
//            food.Origin = foodDto.Origin;
//            food.Image = foodDto.Image;
//            food.Calorie = foodDto.Calorie;

//            // Convert the string value to UnitType enum
//            if (Enum.TryParse(foodDto.Unit, out UnitType unitType))
//            {
//                food.Unit = unitType;
//            }
//            else
//            {
//                return "Invalid unit type.";
//            }

//            food.FoodClassId = foodDto.FoodClassId;

//            try
//            {
//                await _dbContext.SaveChangesAsync();
//                return "Food updated successfully.";
//            }
//            catch (Exception)
//            {
//                return "Food failed to update.";
//            }
//        }


//        public async Task AddFoodClassAsync(FoodClass foodClass)
//        {
//            // Generate a new GUID for the FoodClass entity
//            foodClass.Id = Guid.NewGuid().ToString();

//            // Add the new FoodClass to the database
//            await _dbContext.FoodClasses.AddAsync(foodClass);
//            await _dbContext.SaveChangesAsync();
//        }

//        //get food list by category
//        public async Task<ICollection<FoodDto>> GetFoodByCategory(string id)
//        {

//            var getFoodData = await _dbContext.Foods.Where(x => x.FoodClassId == id).ToListAsync();
//            if (getFoodData.Count() == 0)
//                return null;

//            List<FoodDto> result = new();

//            foreach (var food in getFoodData)
//            {
//                FoodDto newEntry = new()
//                {
//                    Name = food.Name,
//                    Description = food.Description,
//                    Details = food.Details,
//                    Origin = food.Origin,
//                    Image = food.Image,
//                    Calorie = food.Calorie,
//                    Unit = food.Unit,

//                    FoodClassId = food.FoodClassId,
//                };

//                result.Add(newEntry);
//            }

//            return result;
//        }

//        public async Task<FoodClass> GetFoodClassesByIdAsync(string foodClassId)
//        {
//            var res = new FoodClass();
//            var foodClassInfo = await _dbContext.FoodClasses.FirstOrDefaultAsync(fc => fc.Id == foodClassId);
//            if (foodClassInfo != null)
//            {
//                return res;
//            }
//            return foodClassInfo;

//        }

//        public string DeleteFoodClass(string foodClassId)
//        {
//            var foodClass = _dbContext.FoodClasses.FirstOrDefault(fc => fc.Id == foodClassId);

//            if (foodClass != null)
//            {
//                _dbContext.FoodClasses.Remove(foodClass);
//                _dbContext.SaveChanges();

//                return "Delete Successful";
//            }

//            return "Food class does not exist";
//        }

//        public async Task<string> DeleteAsync(string id)
//        {
//            var existingFood = await _dbContext.Foods.FirstOrDefaultAsync(x => x.Id == id);
//            if (existingFood == null)
//            {
//                return $"Food with Id: {id} cannot be found";
//            }
//            _dbContext.Foods.Remove(existingFood);
//            await _dbContext.SaveChangesAsync();
//            return "Successfully deleted";
//        }

//        public async Task<Food> GetFoodByIdAsync(string id)
//        {
//            var food = await _dbContext.Foods.FirstOrDefaultAsync(x => x.Id == id);

//            if (food == null) return null;
//            return food;
//        }

//        public async Task<string> CreateFoodClass(FoodClassDto foodClassDto)
//        {
//            try
//            {
//                var foodClassEntity = new FoodClass
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = foodClassDto.Name,
//                    Description = foodClassDto.Description,
//                    Details = foodClassDto.Details,
//                };

//                _dbContext.FoodClasses.Add(foodClassEntity);
//                await _dbContext.SaveChangesAsync();

//                return "FoodClass Created Successfully";
//            }
//            catch (Exception ex)
//            {
//                return "Failed To Create Foodclass";
//            }
//        }
//    }
//}