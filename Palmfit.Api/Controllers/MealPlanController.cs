using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Core.Implementations;
using Palmfit.Core.Services;
using System.Diagnostics.CodeAnalysis;

namespace Palmfit.Api.Controllers
{
    [Route("api/MealPlanController")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        private readonly IMealPlanRepository _repository;
        public MealPlanController(IMealPlanRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("Add-MealPlan")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMealPlan([FromBody] PostMealDto postMealDto, string foodId, string foodClassId)
        {
            if (postMealDto == null || foodId == null || foodClassId == null)
            {
                return BadRequest(ApiResponse.Failed("Invalid Input"));
            }

			if(postMealDto.Day  < 1 )
			{
				return BadRequest(ApiResponse.Failed(""));
			}

            var result = await _repository.AddMealPlan(postMealDto, foodId, foodClassId);

            if (result == "not found")
                return NotFound();

            return Ok(ApiResponse.Success(postMealDto));
        }
	

		[HttpGet("get-meal-plan-based-on-category")]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetWeeklyPlan(string foodClassId)
		{
			if (foodClassId == null)
			{
				return BadRequest(new ApiResponse<string>("wrong parameter entry!"));
			}

			var result = await _repository.GetWeeklyPlan(foodClassId);
			if (result == null)
			{
				return NotFound(new ApiResponse<string>("meal plan not found!"));
			}

			return Ok(ApiResponse.Success(result));
		}


		[HttpPost("add-selected-plan-for-user")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> AddSelectedMealPlan(string appUserId, string foodClassId)
		{
			if (appUserId == null || foodClassId == null)
			{
				return BadRequest();
			}

			await _repository.AddSelectedMealPlan(appUserId, foodClassId);

			return Ok(ApiResponse.Success("sucessfully added to database!"));	
		}



		[HttpPut("update-selected-meal-plan")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> ModifyMealPlanCategory(string foodClassId, string newFoodId, string appUserId)
		{

			if(foodClassId == null || newFoodId == null || appUserId == null) 
			{
				return BadRequest();
			}

			var result = await _repository.UpdateSelectedMealPlan(foodClassId, newFoodId, appUserId);

			if (result == null)
			{
				return NotFound(ApiResponse.Failed("Failure"));
			}
				

			return Ok(ApiResponse.Success("meal category updated for this user"));
		}


		[HttpDelete("delete-selected-plan")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse<bool>>> DeleteSelectedPlan(string selectedplanId)
		{
			
				var result = await _repository.DeleteSelectedPlanAsync(selectedplanId);

				if (!result)
					return ApiResponse<bool>.Failed(false, "Selected Plan not deleted");

				return ApiResponse<bool>.Success(true, "Selected plan deleted successfully");	
			
		}


		[HttpGet("get-selected-meal-plan")]
		public async Task<IActionResult> GetSelectedPlan(string appUserId)
		{
			if(appUserId == null)
			{
				return BadRequest();
			}

			 var result = await _repository.GetSelectedPlan(appUserId);

			return Ok(ApiResponse.Success(result));


		}

		[HttpGet("/get-weekly-meal-plan-paginated")]
		public async Task<ActionResult<IEnumerable<IEnumerable<MealPlanDto>>>> GetPaginatedWeeklyPlans(string foodClassId, int pageNumber)
		{

			if(foodClassId == null || pageNumber == null)
			{
				return BadRequest(ApiResponse.Failed("failed"));
			}

			var result = await _repository.GetPaginatedWeeklyPlans(foodClassId, pageNumber);

			return Ok(ApiResponse.Success(result));
		}

	}
}
