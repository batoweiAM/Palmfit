using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.EntityEnums;

namespace Palmfit.Api.Controllers
{
    public class CalorieController : ControllerBase
    {
        private readonly ICalorieRepository _calorieRepository;

        public CalorieController(ICalorieRepository calorieRepository)
        {
            _calorieRepository = calorieRepository;
        }

        [HttpGet("calculate-calorie")]
        public ActionResult<ApiResponse<CalorieEstimateDto>> CalculateCalorie(
    string gender, int age, decimal weightKg, decimal heightCm,
    WeightGoal weightGoal)
        {
            // Convert gender string to Gender enum
            Gender genderEnum;
            if (Enum.TryParse<Gender>(gender, out genderEnum))
            {
                decimal bmr = _calorieRepository.CalculateBMR(genderEnum, age, weightKg, heightCm);

                CalorieEstimateDto adjustedCalories = _calorieRepository.CalculateAdjustedCalories(genderEnum, age, weightKg, heightCm, weightGoal);

                return ApiResponse<CalorieEstimateDto>.Success(adjustedCalories, "Calorie calculation successful");
            }
            else
            {
                return ApiResponse<CalorieEstimateDto>.Failed(null, "Invalid gender value.");
            }
        }

    }
}
