using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Data.Entities
{
    public class AppUserRole : IdentityRole<string>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
