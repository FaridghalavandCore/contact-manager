using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.Queries.Contact
{
    public class ContactListRequestDto
    {
        public string Keyword { get; set; }
        public long UserId { get; set; }
    }
}
