using Palmfit.Core.Dtos;
using Palmfit.Data.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Services
{
    public interface ICalorieRepository
    {
        decimal CalculateBMR(Gender gender, int age, decimal weightKg, decimal heightCm);
        decimal CalculateTDEE(decimal bmr, TDEELevel tdeeLevel);
        decimal AdjustCaloriesForWeightGoal(decimal tdee, WeightGoal weightGoal);
        decimal AdjustCaloriesForTDEELevel(decimal bmr, TDEELevel tdeeLevel, WeightGoal weightGoal);
        CalorieEstimateDto CalculateAdjustedCalories(Gender gender, int age, decimal weightKg, decimal heightCm, WeightGoal weightGoal);
    }
}
