using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace DataTransferObject.User
{
    public class CreateTokenDto
    {
        public string Token { get; set; }
        public int TokenExpiry { get; set; }//minute
        public List<Claim> Claims { get; set; }
    }
}
