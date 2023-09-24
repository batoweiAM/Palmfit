using Palmfit.Core.Dtos;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Services
{
    public interface IReviewRepository 
    {
        Task<List<Review>> GetReviewsByUserIdAsync(string userId);

		Task<string> AddReview(ReviewDto reviewDTO, string userId);
        Task<string> DeleteReviewAsync(ClaimsPrincipal loggedInUser, string reviewId);
        Task<string> UpdateReviewAsync(string userId, ReviewDto review);
    }
}
