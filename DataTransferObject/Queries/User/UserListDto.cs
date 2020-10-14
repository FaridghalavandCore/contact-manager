using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.Queries.User
{
   public class UserListDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
