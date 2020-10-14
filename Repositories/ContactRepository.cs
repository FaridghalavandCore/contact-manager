using System.Linq;
using Context;
using DataTransferObject.Commands.Contact;
using DataTransferObject.Queries.Contact;
using DataTransferObject.Shared;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Common.Extentions;

namespace Repositories
{
    public class ContactRepository
    {
        private readonly IContactManagerContext _contactManagerContext;
        public ContactRepository(IContactManagerContext contactManagerContext)
        {
            _contactManagerContext = contactManagerContext;
        }
        public async Task<ListResponseDto<ContactListDto>> List(ListRequestDto<ContactListRequestDto> request)
        {
            var list = await (from u in _contactManagerContext.Contacts
                              where u.IsRemove == false && u.Id != request.Search.UserId
                              select new ContactListDto
                              {
                                  FirstName = u.FirstName,
                                  LastName = u.LastName,
                                  PhoneNumber = u.PhoneNumber,
                              }).GetPaged(request.Page, request.PageSize, null).ToListAsync();
            return new ListResponseDto<ContactListDto>
            {
                List = list,
                PageSize = list.Count(),
                Page = request.Page
            };
        }
        public async Task<ResponseDto> AddValidation(AddContactValidationRequestDto request)
        {
            if (string.IsNullOrEmpty(request.PhoneNumber))
                new ResponseDto { Successful = false, Title = "Please insert phoneNumber" };

            var existContactResult = await ExistContactAsync(new ExistContactRequestDto
            {
                PhoneNumber = request.PhoneNumber
            });

            return new ResponseDto { Successful = existContactResult.Successful, Title = existContactResult.Title };
        }
        public async Task<ResponseDto> Add(AddContactRequestDto request)
        {
            var contact = new Contact
            {
                CreatedAt = DateTime.Now,
                CreatedBy = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                IsRemove = false,
                UserId = request.UserId,
            };
            await _contactManagerContext.Contacts.AddAsync(contact);
            return await _contactManagerContext.SaveChangesAsync() <= 0
                ? new ResponseDto { Successful = false, Title = "Contact not added" }
                : new ResponseDto { Successful = true, Title = "Contact added" };
        }
        private async Task<ResponseDto> ExistContactAsync(ExistContactRequestDto request)
        {
            var existContact = await _contactManagerContext.Contacts.AnyAsync(p => p.PhoneNumber == request.PhoneNumber);
            return existContact
                ? new ResponseDto { Successful = false, Title = "Exist contact" }
                : new ResponseDto { Successful = true, Title = "Dont exist contact" };
        }
    }
}
