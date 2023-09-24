using Palmfit.Core.Dtos;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Services
{
	public interface IUserCalorieDataRepository
	{
		Task AddUserCalorieDataAsync(UserCalorieDataDto userCalorieDataDto);
		Task<AllCalorieData> GetUserCalorieDataByIdAsync(string id);
		Task UpdateUserCalorieDataAsync(UserCalorieDataDto userCalorieDataDto, string userId);
	}
}
