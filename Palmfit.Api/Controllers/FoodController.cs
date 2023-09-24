//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;
//using Palmfit.Core.Dtos;
//using Palmfit.Core.Services;
//using Microsoft.EntityFrameworkCore;
//using Palmfit.Core.Implementations;
//using Palmfit.Data.AppDbContext;
//using Palmfit.Data.Entities;
//using static System.Runtime.InteropServices.JavaScript.JSType;
//using System;
//using Palmfit.Data.EntityEnums;



//namespace Palmfit.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FoodController : ControllerBase
//    {
//  //      private readonly PalmfitDbContext _db;
//  //      private readonly IFoodInterfaceRepository _foodRepo;

//		//public FoodController(PalmfitDbContext db, IFoodInterfaceRepository foodRepo)
//		//{
//		//	_db = db;
//		//	_foodRepo = foodRepo;
//		//}

//		//[HttpGet("get-all-meals")]
//  //      public async Task<ActionResult<IEnumerable<FoodDto>>> GetAllFoods()
//  //      {
//  //          //Getting all food from database
//  //          var foods = await _foodRepo.GetAllFoodAsync();
//  //          if (!foods.Any())
//  //          {
//  //              return NotFound(ApiResponse.Failed("Food does not exist"));
//  //          }

//  //          var result = await _foodRepo.GetAllFoodAsync();

//  //          return (Ok(ApiResponse.Success(result)));
//  //      }


//  //      [HttpPut("{foodClassId}/update-foodclass")]
//  //      public async Task<ActionResult<ApiResponse<FoodClassDto>>> UpdateFoodClass(string foodClassId, [FromBody] FoodClassDto updatedFoodClassDto)
//  //      {
//  //          var response = await _foodRepo.UpdateFoodClass(foodClassId, updatedFoodClassDto);
//  //          if (response == null)
//  //          {
//  //              return BadRequest(ApiResponse.Failed(response));
//  //          }
//  //          return Ok(ApiResponse.Success(response));
//  //      }

//  //      [HttpGet("get-meal-Id")]
//  //      public async Task<IActionResult> GetFoodById(string Id)
//  //      {
            
//  //          {
//  //              var meal = await _foodRepo.GetFoodById(Id);

//  //              if (meal == null)
//  //              {
//  //                  return NotFound(ApiResponse.Failed("Meal not found"));
//  //              }

//  //              var mealDto = new FoodDto
//  //              {
//  //                  Name = meal.Name,
//  //                  Description = meal.Description,
//  //                  Image = meal.Image
//  //              };

//  //              return Ok(ApiResponse.Success( mealDto));
//  //          }
            
            
//  //      }

        

//  //      /* < Start----- required methods to Calculate Calorie -----Start > */

//  //      [HttpGet("calculate-calorie-by-name")]

//        public async Task<ActionResult<ApiResponse<CalorieDto>>> CalculateCalorieForFoodByName(string foodName, UnitType unit, decimal amount)
//        {
//            try
//            {
//                var calorie = await _foodRepo.GetCalorieByNameAsync(foodName, unit, amount);
//                return ApiResponse<CalorieDto>.Success(calorie, "Calorie calculation successful");
//            }
//            catch (ArgumentException ex)
//            {
//                return ApiResponse<CalorieDto>.Failed(null, ex.Message);
//            }
//        }

//  //      [HttpGet("calculate-total-calorie")]
//  //      public async Task<ActionResult<ApiResponse<decimal>>> CalculateTotalCalorieForSelectedFoods([FromQuery] Dictionary<string, (UnitType unit, decimal amount)> foodNameAmountMap)
//  //      {
//  //          if (foodNameAmountMap == null || !foodNameAmountMap.Any())
//  //          {
//  //              return ApiResponse<decimal>.Failed(0, "Food IDs and units dictionary cannot be empty.");
//  //          }

//  //          try
//  //          {
//  //              decimal totalCalorie = await _foodRepo.CalculateTotalCalorieAsync(foodNameAmountMap);
//  //              return ApiResponse<decimal>.Success(totalCalorie, "Total calorie calculation successful");
//  //          }
//  //          catch (ArgumentException ex)
//  //          {
//  //              return ApiResponse<decimal>.Failed(0, ex.Message);
//  //          }
//  //      }


//        //[HttpPost("add-food")]
//        //public async Task<ActionResult<ApiResponse<Food>>> AddFood(FoodDto foodDto)
//        //{
//        //    try
//        //    {
//        //        // Check if FoodClass needs to be added
//        //        FoodClass foodClass = null;
//        //        if (foodDto.FoodClass != null)
//        //        {
//        //            foodClass = new FoodClass
//        //            {
//        //                Name = foodDto.Name,
//        //                Description = foodDto.Description,
//        //                Details = foodDto.Details
//        //            };

//  //                  // Add the new FoodClass to the database
//  //                  await _foodRepo.AddFoodClassAsync(foodClass);
//  //              }

//  //              // Convert the FoodDto to the Food entity
//  //              var food = new Food
//  //              {
//  //                  Name = foodDto.Name,
//  //                  Description = foodDto.Description,
//  //                  Details = foodDto.Details,
//  //                  Origin = foodDto.Origin,
//  //                  Image = foodDto.Image,
//  //                  Calorie = foodDto.Calorie,
//  //                  Unit = foodDto.Unit,
//  //                  FoodClass = foodClass,
//  //                  CreatedAt = DateTime.UtcNow,
//  //                  UpdatedAt = DateTime.UtcNow
//  //              };

//  //              // Add the new food to the database
//  //              await _foodRepo.AddFoodAsync(food);

//  //              return ApiResponse<Food>.Success(food, "Food added successfully");
//  //          }
//  //          catch (Exception ex)
//  //          {
//  //              return ApiResponse<Food>.Failed(null, ex.Message);
//  //          }
//  //      }

//  //      /* < End----- required methods to Calculate Calorie -----End > */



//  //      [HttpGet("foods-based-on-class")]
//  //      public async Task<IActionResult> GetFoodsBasedOnClass(string id)
//  //      {
//  //          if (id == null) return BadRequest(ApiResponse.Failed(null, "Invalid id"));

//  //          var result = await _foodRepo.GetFoodByCategory(id);

//  //          if (result == null)
//  //              return NotFound(ApiResponse.Failed(result));

//  //          return Ok(ApiResponse.Success(result));
//  //      }


//  //      [HttpDelete("{id}/Delete-Food-byId")]
//  //      public async Task<ActionResult<ApiResponse>> DeleteAsync([FromRoute] string id)
//  //      {

//  //          var targetedFood = await _foodRepo.GetFoodByIdAsync(id);

//  //          if (targetedFood == null)
//  //          {
           
//  //              return NotFound(ApiResponse.Failed("Food not found"));   // Provide a response indicating Failed deletion if food does not exist
//  //          } 
//  //          await _foodRepo.DeleteAsync(id);
//  //          return ApiResponse.Success("Food deleted Successfully");     // Provide a response indicating successful deletion 
//  //      }

//  //      //api-to-updatefood
//  //      [HttpPut("update-food")]
//  //      public async Task<IActionResult> UpdateFood(string id, UpdateFoodDto foodDto)
//  //      {
//  //          var updatedfood = await _foodRepo.UpdateFoodAsync(id, foodDto);
//  //          if (updatedfood == "Food not found.")
//  //              return NotFound(ApiResponse.Failed(updatedfood));
//  //          else if (updatedfood == "Food failed to update.")
//  //          {
//  //              return BadRequest(ApiResponse.Failed(updatedfood));
//  //          }

//  //          return Ok(ApiResponse.Success(updatedfood));
//  //      }


	
//  //      [HttpGet("Get-FoodClass-By-Id")]
//  //      public async Task<ActionResult<ApiResponse<FoodClass>>> GetFoodClassById(string foodClassId)
//  //      {
//  //          try
//  //          {
//  //              var existingFoodClass = await _foodRepo.GetFoodClassesByIdAsync(foodClassId);

//  //              if (existingFoodClass.Id == null)
//  //              {
//  //                  // Food class not found, return an error response
//  //                  return ApiResponse<FoodClass>.Failed(data: null, message: "Food class not found");
//  //              }

//  //              // Return the food class as a success response
//  //              return ApiResponse<FoodClass>.Success(existingFoodClass, message: "Food class retrieved successfully");
//  //          }
//  //          catch (Exception ex)
//  //          {
//  //              // If an exception occurs during the retrieval process, return an error response
//  //              return ApiResponse<FoodClass>.Failed(data: null, message: "An error occurred while retrieving the food class.", errors: new List<string> { ex.Message });
//  //          }
//  //      }




//  //      [HttpDelete("delete-foodclass")]
//  //      public async Task<ActionResult<ApiResponse<string>>> DeleteFoodClass(string foodClassId)
//  //      {
//  //          try
//  //          {
//  //              var existingFoodClass = _foodRepo.DeleteFoodClass(foodClassId);

//  //              if (existingFoodClass == "Food class does not exist")
//  //              {
//  //                  // Food class not found, return an error response
//  //                  return ApiResponse<string>.Failed(data: null, message: "Food class not found");
//  //              }

//  //              _foodRepo.DeleteFoodClass(foodClassId);

//  //              // Return a success response
//  //              return ApiResponse<string>.Success(data: null, message: "Food class deleted successfully");
//  //          }
//  //          catch (Exception ex)
//  //          {
//  //              // If an exception occurs during the deletion process, return an error response
//  //              return ApiResponse<string>.Failed(data: null, message: "An error occurred during food class deletion.", errors: new List<string> { ex.Message });
//  //          }
//  //      }

//  //      [HttpPost("create-class-of-food")]
//  //      public async Task<IActionResult> CreateFoodClass(FoodClassDto foodClassDto)
//  //      {
//  //          if (!ModelState.IsValid)
//  //          {
//  //              return BadRequest(ModelState);
//  //          }

//  //          var createdFoodClass = await _foodRepo.CreateFoodClass(foodClassDto);

//  //          if (createdFoodClass == null)
//  //          {
//  //              return NotFound(ApiResponse.Failed(createdFoodClass));
//  //          }
//  //          return Ok(ApiResponse.Success(createdFoodClass));

//  //      }

////        [HttpGet("search-food")]
////        [ProducesResponseType(StatusCodes.Status200OK)]
////        [ProducesResponseType(StatusCodes.Status400BadRequest)]
////        [ProducesResponseType(StatusCodes.Status404NotFound)]
////        public async Task<IActionResult> SearchFood([FromQuery] string searchTerms)
////        {
////            try
////            {
////                var result = await _foodRepo.SearchFood(searchTerms);
////                if (result.Any())
////                {
////                    return Ok(ApiResponse.Success(result));
////                }
////                return NotFound(ApiResponse.Failed(new List<Food>(), "Food not found."));
////            }
////            catch (Exception ex)
////            {
////                return BadRequest(ex.Message);
////            }
////        }
////    }
////}
        
    


        