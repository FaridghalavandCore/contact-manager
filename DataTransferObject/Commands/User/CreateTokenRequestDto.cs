using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.User
{
    public class CreateTokenRequestDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
