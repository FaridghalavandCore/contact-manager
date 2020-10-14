using Common.Extentions;
using DataTransferObject.Commands.Contact;
using DataTransferObject.Shared;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Command
{
    public class ContactCommand : IContactCommand
    {
        private readonly IUnitOfWork _unitOfWork; 
        public ContactCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto> Add(AddContactRequestDto request)
        {

            var validateContactResult = await _unitOfWork.ContactRepository.AddValidation(new AddContactValidationRequestDto
            {
                PhoneNumber= request.PhoneNumber,
            });
            if (!validateContactResult.Successful)
                return new ResponseDto { Successful = validateContactResult.Successful, Title = validateContactResult.Title };

            var result = await _unitOfWork.ContactRepository.Add(request);

            return result;

        }
    }
}
