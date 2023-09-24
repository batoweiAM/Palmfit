using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Data.Entities
{
    public class AppUserPermission
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string Name { get; set; }
    }
}
