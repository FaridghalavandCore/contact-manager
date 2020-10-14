using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.Commands.Contact
{
    public class AddContactRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public long UserId { get; set; }
    }
}
