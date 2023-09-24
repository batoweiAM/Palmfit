using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
    public class InviteRepository : IInviteRepository
    {
        //props
        private readonly PalmfitDbContext _dbContext;

        //ctor
        public InviteRepository(PalmfitDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //delete an invite
        public async Task<bool> Deleteinvite(string id)
        {
            var getData = await _dbContext.Invites.FirstOrDefaultAsync(x => x.Id == id);

            if (getData == null) return false;

            getData.IsDeleted = true;
            var result = _dbContext.SaveChanges();
            
            if (result > 0) return true;

            return false;
        }
        public async Task<List<InviteDto>> GetInvitesByUserId(string userId)
        {
            var invites = await _dbContext.Invites
                .Where(invite => invite.AppUserId == userId)
                .Select(invite => new InviteDto
                {
                    Date = invite.Date,
                    Name = invite.Name,
                    Email = invite.Email,
                    Phone = invite.Phone
                })
                .ToListAsync();

            return invites;

        }

        public async Task<PaginParameter<Invite>> GetAllUserInvitesAsync(int page, int pageSize)
        {
            // Calculate skip for pagination
            int skip = (page - 1) * pageSize;

            // Query user invites including related AppUser
            var userInvitesQuery = await _dbContext.Invites
                .OrderByDescending(ui => ui.CreatedAt)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            // Calculate total count
            int totalCount = await _dbContext.Invites.CountAsync();

            // Create and return paginated response
            return new PaginParameter<Invite>
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = userInvitesQuery
            };
        }
    }
}
