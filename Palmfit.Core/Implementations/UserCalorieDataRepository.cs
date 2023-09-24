using Microsoft.EntityFrameworkCore;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.AppDbContext;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Implementations
{
    public class UserCalorieDataRepository : IUserCalorieDataRepository
    {
        private readonly PalmfitDbContext _dbContext;

        public UserCalorieDataRepository(PalmfitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserCalorieDataAsync(UserCalorieDataDto userCalorieDataDto) 
        {
            var data = new AllCalorieData() 
            {
                
                WeightGoal = userCalorieDataDto.WeightGoal,
                ActivityLevel = userCalorieDataDto.ActivityLevel,
                Age = userCalorieDataDto.Age,
                Height = userCalorieDataDto.Height,
                Weight = userCalorieDataDto.Weight,
                Gender = userCalorieDataDto.Gender,
                AppUserId = userCalorieDataDto.AppUserId,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,   
                UpdatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid().ToString(),
                
            };
            await _dbContext.AllCalorieInfos.AddAsync(data);
            _dbContext.SaveChanges();
        }



        public async Task<AllCalorieData> GetUserCalorieDataByIdAsync(string id)
        {
            return await _dbContext.AllCalorieInfos.FirstOrDefaultAsync(user => user.AppUserId == id);
        }




        public async Task UpdateUserCalorieDataAsync(UserCalorieDataDto userCalorieDataDto, string userId)

		{

			var domainUserCalorieData = await _dbContext.userCalorieTable.SingleOrDefaultAsync(user => user.AppUserId == userId);

			if (domainUserCalorieData != null)

			{

				domainUserCalorieData.WeightGoal = userCalorieDataDto.WeightGoal;

				domainUserCalorieData.ActivityLevel = userCalorieDataDto.ActivityLevel;

				domainUserCalorieData.Age = userCalorieDataDto.Age;

				domainUserCalorieData.Height = userCalorieDataDto.Height;

				domainUserCalorieData.Weight = userCalorieDataDto.Weight;

				domainUserCalorieData.Gender = userCalorieDataDto.Gender;

				domainUserCalorieData.UpdatedAt = DateTime.UtcNow;

			}

			await _dbContext.SaveChangesAsync();

		}

	}
}
