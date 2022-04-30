using Microsoft.AspNetCore.Identity;

namespace Climapi.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Queries = new HashSet<QueryRecord>();
            UserAppRoles = new HashSet<AppRole>();
        }

        public virtual ICollection<AppRole> UserAppRoles { get; set; }
        public virtual ICollection<QueryRecord> Queries { get; set; }
    }
}
