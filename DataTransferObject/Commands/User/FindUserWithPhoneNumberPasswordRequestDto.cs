using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.User
{
    public class FindUserWithPhoneNumberPasswordRequestDto
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
