using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.Queries.User
{
    public class UserListRequestDto
    {
        public string Keyword { get; set; }
        public long UserId { get; set; }
    }
}
