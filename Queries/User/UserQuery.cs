using Context;
using DataTransferObject.Queries.User;
using DataTransferObject.Shared;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Queries.User
{
    public class UserQuery : IUserQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ListResponseDto<UserListDto>> List(ListRequestDto<UserListRequestDto> request)
        {
            var list = await _unitOfWork.UserRepository.List(request);
            return list;
        }
    }
}
