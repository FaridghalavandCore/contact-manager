using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.User
{
    public class UserAddRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long Creator { get; set; }
    }
}
