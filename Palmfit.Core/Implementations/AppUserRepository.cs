using Microsoft.AspNetCore.Identity;
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
using System.Transactions;

namespace Palmfit.Core.Implementations
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly PalmfitDbContext _dbContext;

        public AppUserRepository(UserManager<AppUser> userManager, PalmfitDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> CreateUser(SignUpDto userRequest)
        {
            var user = await _userManager.FindByEmailAsync(userRequest.Email);
            if (user != null) return ApiResponse.Failed("User already exist");
            user = new AppUser()
            {
                FirstName = userRequest.Firstname,
                LastName = userRequest.Lastname,
                Email = userRequest.Email,
                UserName = userRequest.Email
            };

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var createUser = await _userManager.CreateAsync(user, userRequest.Password);
                if (!createUser.Succeeded) return ApiResponse.Failed(createUser.Errors);
                transaction.Complete();
                return ApiResponse.Success("User added successfully");
            }
        }

        public async Task<AppUser> GetUserById(string userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }
    }
}