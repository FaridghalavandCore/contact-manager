using Common.Extentions;
using Context;
using DataTransferObject.Enums;
using DataTransferObject.Queries.User;
using DataTransferObject.Shared;
using DataTransferObject.User;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository
    {
        private readonly IContactManagerContext _contactManagerContext;
        public UserRepository(IContactManagerContext contactManagerContext)
        {
            _contactManagerContext = contactManagerContext;
        }

        public async Task<ResponseDto<UserDto>> FindUserWithPhoneNumberPassword(FindUserWithPhoneNumberPasswordRequestDto request)
        {
            var user = await _contactManagerContext.Users
                .Where(p => p.PhoneNumber == request.PhoneNumber && p.Password == request.Password)
                .Select(p => new UserDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    CreatedAt = p.CreatedAt,
                    Email = p.Email,
                    UserType = p.UserType
                }).FirstOrDefaultAsync();
            return user == null
            ? new ResponseDto<UserDto> { Successful = false, Title = "user not found" }
            : new ResponseDto<UserDto> { Response = user, Successful = true, Title = "find user" };
        }
        public async Task<ResponseDto> Add(UserAddRequestDto request)
        {
            var user = new User
            {
                CreatedAt = DateTime.Now,
                CreatedBy = request.Creator,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password,
                LastName = request.LastName,
                FirstName = request.FirstName,
                IsRemove = false,
                UserType = UserType.User
            };
            await _contactManagerContext.Users.AddAsync(user);
            return await _contactManagerContext.SaveChangesAsync() <= 0
            ? new ResponseDto { Successful = false, Title = "User not added" }
            : new ResponseDto { Successful = true, Title = "User added" };
        }
        public async Task<ResponseDto> UserAddValidation(UserAddValidationRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Phonenumber))
                new ResponseDto { Successful = false, Title = "Please insert phoneNumber" };

            var existPhoneNumberResult = await ExistPhoneNumberAsync(new ExistphoneNumberRequestDto { PhoneNumber = request.Phonenumber });
            if (!existPhoneNumberResult.Successful)
                new ResponseDto { Successful = existPhoneNumberResult.Successful, Title = existPhoneNumberResult.Title };

            return string.IsNullOrEmpty(request.Password)
                ? new ResponseDto { Successful = false, Title = "Please insert password" }
                : new ResponseDto { Successful = true, Title = "validate successful" };
        }
        public async Task<ResponseDto> ExistPhoneNumberAsync(ExistphoneNumberRequestDto request)
        {
            var existPhoneNumber = await _contactManagerContext.Users.AnyAsync(p => p.PhoneNumber == request.PhoneNumber);
            return existPhoneNumber
                ? new ResponseDto { Successful = false, Title = "Exist phoneNumber" }
                : new ResponseDto { Successful = true, Title = "Dont exist phoneNumber" };
        }
        public async Task<ListResponseDto<UserListDto>> List(ListRequestDto<UserListRequestDto> request)
        {
            var list = await (from u in _contactManagerContext.Users
                               where u.IsRemove == false && u.Id != request.Search.UserId
                               select new UserListDto
                               {
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   PhoneNumber = u.PhoneNumber,
                                   Email = u.Email,
                               }).GetPaged(request.Page,request.PageSize,null).ToListAsync();
            return new ListResponseDto<UserListDto>
            {
                List =list,
                PageSize = list.Count(),
                Page = request.Page
            };
        }
    }
}

