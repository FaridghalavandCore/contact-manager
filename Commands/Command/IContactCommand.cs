using DataTransferObject.Commands.Contact;
using DataTransferObject.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Command
{
    public interface IContactCommand
    {
        Task<ResponseDto> Add(AddContactRequestDto request);
    }
}
