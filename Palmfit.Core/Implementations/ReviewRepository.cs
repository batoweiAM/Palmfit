using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.AppDbContext;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly PalmfitDbContext _dbContext;
        private readonly PalmfitDbContext _palmfitDb;
        private readonly UserManager<AppUser> _userManager;

		public ReviewRepository(PalmfitDbContext dbContext, PalmfitDbContext palmfitDb, UserManager<AppUser> userManager)
		{
			_dbContext = dbContext;
			_palmfitDb = palmfitDb;
			_userManager = userManager;
		}

		public async Task<string> AddReview(ReviewDto reviewDTO, string userId)
		{
			try
			{
				var validateUser = await _userManager.FindByIdAsync(userId);

				if (validateUser == null)
				{
					return null;
				}

				Review review = new Review()
				{
					Date = DateTime.Now.Date,
					Comment = reviewDTO.Comment,
					Rating = reviewDTO.Rating,
					Verdict = reviewDTO.Verdict,
					AppUserId = userId,
					Id = Guid.NewGuid().ToString(),
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now,
					IsDeleted = false
				};

				_dbContext.Reviews.Add(review);
				_dbContext.SaveChanges();

				return "Review Updated Sucessfully!";
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			
		}


		public async Task<List<Review>> GetReviewsByUserIdAsync(string userId)
		{
			var reviewsDto = new ReviewDto();
			var reviews = new Review
			{
				Date = reviewsDto.Date,
				Comment = reviewsDto.Comment,
				Rating = reviewsDto.Rating,
				Verdict = reviewsDto.Verdict,
				AppUserId = reviewsDto.AppUserId
			};

			var ReviewResult = await _dbContext.Reviews.Where(r => r.AppUserId == userId).ToListAsync();
			if (!ReviewResult.Any())
			{
				return new List<Review>();
			}
			return ReviewResult;

		}


        public async Task<string> DeleteReviewAsync(ClaimsPrincipal loggedInUser, string reviewId)
        {
            var user = loggedInUser.FindFirst(ClaimTypes.NameIdentifier);
            var review = await _palmfitDb.Reviews.FindAsync(reviewId);

            string message = string.Empty;
            if (user == null)
            {
                message = "User not found";
            }
            else if (review.AppUserId != user.Value)
            {
                message = "You are not authorized to delete this review";
            }
            else if (review == null)
            {
                message = "Review not found";
            }
            else
            {
                review.IsDeleted = true;
                await _palmfitDb.SaveChangesAsync();
                message = "Review deleted successful";
            }
            return message;
        }

        public async Task<string> UpdateReviewAsync(string userId, ReviewDto review)
        {
            var reviewDto = new ReviewDto()
            {
                Date = review.Date,
                Comment = review.Comment,
                Verdict = review.Verdict,
                AppUserId = review.AppUserId,
            };

            var existingReview = await _dbContext.Reviews.FindAsync(userId);

            if (existingReview == null)
            {
                return "Review not found";
            }

            // Update properties from DTO
            existingReview.Date = reviewDto.Date;
            existingReview.Comment = reviewDto.Comment;
            existingReview.Rating = reviewDto.Rating;
            existingReview.Verdict = reviewDto.Verdict;

            // Mark the entity as modified
            _dbContext.Entry(existingReview).State = EntityState.Modified;


            await _dbContext.SaveChangesAsync();
            return "Review updated successfully";
        }
    }
}
