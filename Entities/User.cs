using DataTransferObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class User
    {
        public long Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [RegularExpression(@"(0)?([ ]|-|[()]){0,2}9[1|2|3|4|6|7|8|9]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}")]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
        public bool IsRemove { get; set; }

        #region .:: Relations ::.
        public ICollection<Contact> Contacts { get; set; }

        #endregion
    }
}
