using DataTransferObject.Queries.Contact;
using DataTransferObject.Shared;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Contact
{
    public class ContactQuery : IContactQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ListResponseDto<ContactListDto>> List(ListRequestDto<ContactListRequestDto> request)
        {
            var list = await _unitOfWork.ContactRepository.List(request);
            return list;
        }
    }
}
