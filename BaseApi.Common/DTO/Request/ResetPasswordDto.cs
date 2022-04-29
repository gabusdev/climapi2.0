using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climapi.Common.DTO.Request
{
    public class ResetPasswordDto
    {
        public string? Email { get; set; }
        
        public string? ResetToken { get; set; }

        public string? NewPassword { get; set; }

        public string? ConfirmationPassword { get; set; }
    }
}
