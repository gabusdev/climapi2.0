using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climapi.Core.Entities
{
    public record QueryRecord
    {
        public int Id { get; set; }

        public string Query { get; init; } = null!;

        public DateTime Time { get; set; }
        
        public string UserId { get; set; } = null!;
        
        public virtual User User { get; set; } = null!;
    }
}
