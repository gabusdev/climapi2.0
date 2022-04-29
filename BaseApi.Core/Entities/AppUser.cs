using Microsoft.AspNetCore.Identity;

namespace Climapi.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Queries = new HashSet<QueryRecord>();
        }

        public ICollection<QueryRecord> Queries { get; set; }
    }
}
