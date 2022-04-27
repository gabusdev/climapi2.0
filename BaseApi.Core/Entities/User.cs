using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climapi.Core.Entities
{
    public class User: IdentityUser
    {
        public User()
        {
            Queries = new HashSet<QueryRecord>();
        }

        public ICollection<QueryRecord> Queries { get; set; }
    }
}
