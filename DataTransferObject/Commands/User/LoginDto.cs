using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace DataTransferObject.User
{
    public class LoginDto
    {
        public long Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public int TokenExpiry { get; set; }
        public string Role { get; set; }
    }
}
