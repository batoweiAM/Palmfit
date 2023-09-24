using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Implementations
{
    public class CalorieRepository : ICalorieRepository
    {
        public decimal CalculateBMR(Gender gender, int age, decimal weightKg, decimal heightCm)
        {
            if (gender == Gender.Male)
            {
                return (10 * weightKg) + (6.25m * heightCm) - (5 * age) + 5;
            }
            else
            {
                return (10 * weightKg) + (6.25m * heightCm) - (5 * age) - 161;
            }
        }

        public decimal CalculateTDEE(decimal bmr, TDEELevel tdeeLevel)
        {
            decimal tdeeMultiplier;
            switch (tdeeLevel)
            {
                case TDEELevel.Inactive:
                    tdeeMultiplier = 1.2m;
                    break;
                case TDEELevel.SomewhatActive:
                    tdeeMultiplier = 1.55m;
                    break;
                case TDEELevel.Active:
                    tdeeMultiplier = 2.2m;
                    break;
                default:
                    tdeeMultiplier = 1.2m; // Default to Inactive
                    break;
            }

            return bmr * tdeeMultiplier;
        }

        public decimal AdjustCaloriesForWeightGoal(decimal tdee, WeightGoal weightGoal)
        {
            switch (weightGoal)
            {
                case WeightGoal.Lose:
                    return tdee - 500m; 
                case WeightGoal.Maintain:
                    return tdee;
                case WeightGoal.Gain:
                    return tdee + 300m; 
                default:
                    return tdee; 
            }
        }

        // Helper method to round a value to the nearest multiple
        public decimal RoundToNearest(decimal value, int multiple)
        {
            return Math.Round(value / multiple) * multiple;
        }
        public decimal AdjustCaloriesForTDEELevel(decimal bmr, TDEELevel tdeeLevel, WeightGoal weightGoal)
        {
            decimal tdee = CalculateTDEE(bmr, tdeeLevel);
            decimal adjustedCalorie = AdjustCaloriesForWeightGoal(tdee, weightGoal);
            return RoundToNearest(adjustedCalorie,100);
        }
        public CalorieEstimateDto CalculateAdjustedCalories(Gender gender, int age, decimal weightKg, decimal heightCm, WeightGoal weightGoal)
        {
            CalorieEstimateDto adjustedCalories = new CalorieEstimateDto();

            var bmr = CalculateBMR(gender, age, weightKg, heightCm);

            adjustedCalories.Inactive = AdjustCaloriesForTDEELevel(bmr, TDEELevel.Inactive, weightGoal);
            adjustedCalories.SomewhatActive = AdjustCaloriesForTDEELevel(bmr, TDEELevel.SomewhatActive, weightGoal);
            adjustedCalories.Active = AdjustCaloriesForTDEELevel(bmr, TDEELevel.Active, weightGoal);

            return adjustedCalories;
        }
    }


}

