using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DataTransferObject.User
{
    public class LoginRequestDto
    {
        [DisplayName("موبایل")]
        public string PhoneNumber { get; set; }
        [DisplayName("کلمه عبور")]
        public string Password { get; set; }
    }
}
