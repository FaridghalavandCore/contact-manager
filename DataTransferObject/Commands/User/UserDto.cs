using DataTransferObject.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.User
{
    public class UserDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
