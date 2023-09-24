using Microsoft.EntityFrameworkCore;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.AppDbContext;
using Palmfit.Data.Entities;
using System.Security.Claims;

namespace Palmfit.Core.Implementations
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly PalmfitDbContext _palmfitDb;
        private readonly PalmfitDbContext _db;

        public SubscriptionRepository(PalmfitDbContext db)
        {
            _db = db;
        }

        public async Task<bool> DeleteSubscriptionAsync(string subscriptionId)
        {
            var subscription = _db.Subscriptions.FirstOrDefault(s => s.Id == subscriptionId);

            if (subscription == null)
                return await Task.FromResult(false);

            _db.Remove(subscription);

            return await Task.FromResult(true);
        }
        public async Task<Subscription> GetSubscriptionByIdAsync(string subscriptionId)
        {
            return await Task.FromResult(_db.Subscriptions.FirstOrDefault(s => s.Id == subscriptionId));
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptionsByUserIdAsync(string userId)
        {
            return await _db.Subscriptions.Where(s => s.AppUserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptionsByUserNameAsync(string userName)
        {
            return await _db.Subscriptions.Where(s => s.AppUser.UserName == userName).ToListAsync();
        }



        public async Task<Subscription> CreateSubscriptionAsync(CreateSubscriptionDto subscriptionDto, ClaimsPrincipal loggedInUser)
        {
            var subscription = new Subscription
            {
                Id = Guid.NewGuid().ToString(),
                Type = subscriptionDto.Type,
                StartDate = subscriptionDto.StartDate,
                EndDate = subscriptionDto.EndDate,
                AppUserId = loggedInUser.FindFirst(ClaimTypes.NameIdentifier).Value
            };

            _palmfitDb.Subscriptions.Add(subscription);
            await _palmfitDb.SaveChangesAsync();
            _db.Subscriptions.Add(subscription);
            await _db.SaveChangesAsync();

            return subscription;
        }

       
        public async Task<string> UpdateSubscriptionAsync(SubscriptionDto subscriptionDto)
		{
			string message = "";
			var subscription = await _db.Subscriptions.FirstOrDefaultAsync(s => s.Id == subscriptionDto.SubscriptionId);
			if (subscription == null)
			{
				message = "Subscription not found.";
			}
			else
			{
				 
				subscription.Type = subscriptionDto.Type;
				subscription.StartDate = subscriptionDto.StartDate;
				subscription.EndDate = subscriptionDto.EndDate;
				subscription.IsExpired = subscriptionDto.IsExpired;
				subscription.UpdatedAt = DateTime.Now;

				await _db.SaveChangesAsync();
				message = "Subscription updated successfully!";
			}
			return message;
		}

        public async Task<Subscription> GetUserSubscriptionStatusAsync(string userId)
        {
            {
                return await _db.Subscriptions.FirstOrDefaultAsync(sub => sub.AppUserId == userId);
            }

		}
	}
}