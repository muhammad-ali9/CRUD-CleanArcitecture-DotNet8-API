using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AuthenticationResponse
    {
        public string Email { get; set; }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public List<string> Role { get; set; }
        public bool isVerified { get; set; }

        public string JwtToken { get; set; }
    }


}
