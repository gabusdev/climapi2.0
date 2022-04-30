using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climapi.Core.Entities
{
    public class AppRole: IdentityRole
    {
        public AppRole()
        {
            // AppUsers = new HashSet<AppUser>();
        }
        public int DalyRequests { get; set; }
        public int WeeklyRequests { get; set; }
        public int MonthlyRequests { get; set; }

        // public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
