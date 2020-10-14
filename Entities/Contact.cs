using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class Contact
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [RegularExpression(@"(0)?([ ]|-|[()]){0,2}9[1|2|3|4|6|7|8|9]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}")]
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
        public bool IsRemove { get; set; }

        #region .:: Relations ::.
        public virtual User User { get; set; }
        #endregion
    }
}
