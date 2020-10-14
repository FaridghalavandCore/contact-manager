using DataTransferObject.Queries.User;
using DataTransferObject.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Queries.User
{
    public interface IUserQuery
    {
        Task<ListResponseDto<UserListDto>> List(ListRequestDto<UserListRequestDto> request);
    }
}
