using DataTransferObject.Queries.Contact;
using DataTransferObject.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Contact
{
    public interface IContactQuery
    {
        Task<ListResponseDto<ContactListDto>> List(ListRequestDto<ContactListRequestDto> request);
    }
}
