using Core.Helpers;
using Palmfit.Core.Dtos;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Services
{
    public interface IInviteRepository
    {
        Task<PaginParameter<Invite>> GetAllUserInvitesAsync(int page, int pageSize);
        Task<List<InviteDto>> GetInvitesByUserId(string userId);
        Task<bool> Deleteinvite(string id);
    }
}
