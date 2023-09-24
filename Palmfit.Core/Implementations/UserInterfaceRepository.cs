
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
    public class UserInterfaceRepository : IUserInterfaceRepository
    {

        private readonly PalmfitDbContext _db;

        public UserInterfaceRepository(PalmfitDbContext db)
        {
            _db = db;
        }
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _db.Users.ToListAsync();

            return users.Select(user => new UserDto
            {
                Title = user.Title,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Image = user.Image,
                Address = user.Address,
                Area = user.Area,
                State = user.State,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                Country = user.Country,
            }).ToList();
        }
        public async Task<string> UpdateUserAsync(string id, UserDto userDto)
        {
            var user = await _db.Users.FindAsync(id);

            if (user == null)
                return "User not found.";

            user.Title = user.Title;
            user.FirstName = user.FirstName;
            user.MiddleName = user.MiddleName;
            user.LastName = user.LastName;
            user.Image = user.Image;
            user.Address = user.Address;
            user.Area = user.Area;
            user.State = user.State;
            user.Gender = user.Gender;
            user.DateOfBirth = user.DateOfBirth;
            user.Country = user.Country;

            try
            {
                await _db.SaveChangesAsync();
                return "User updated successfully.";
            }
            catch (Exception)
            {
                return "User failed to update.";
            }
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Title = user.Title,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Image = user.Image,
                Address = user.Address,
                Area = user.Area,
                State = user.State,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                Country = user.Country
            };
        }
        public async Task<bool> DeleteUserAsync(string userId)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (user != null)
                {
                    _db.Users.Remove(user); 
                    await _db.SaveChangesAsync(); 

                    return true;
                }
                else
                {
                    Console.WriteLine($"User not found with ID: {userId}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the user: {ex.Message}");
                return false;
            }
        }


        //get user status
        public async Task<UserInfoDto> GetUserStatus(string id)
        {
            
            var getData = await _db.Subscriptions.Include(u => u.AppUser).OrderByDescending(o => o.EndDate).FirstOrDefaultAsync(x => x.Id == id);
            
            if (getData == null) return null;

            UserInfoDto userInfo = new UserInfoDto
            {
                LastOnline = getData.AppUser.LastOnline,
                IsVerified = getData.AppUser.IsVerified,
                Active = getData.AppUser.Active,
                ReferralCode = getData.AppUser.ReferralCode,
                InviteCode = getData.AppUser.InviteCode,
                Type = getData.Type,
                IsExpired = getData.IsExpired,
                EndDate = getData.EndDate
            };

            return userInfo;

        }
     

    }
}
