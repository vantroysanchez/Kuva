using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AuthenticationAnswer
    {
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
