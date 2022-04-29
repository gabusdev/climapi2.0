using Microsoft.AspNetCore.Identity;

namespace Climapi.Core.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            Queries = new HashSet<QueryRecord>();
        }

        public ICollection<QueryRecord> Queries { get; set; }
    }
}
