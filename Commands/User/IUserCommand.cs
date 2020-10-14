using DataTransferObject.Shared;
using DataTransferObject.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commands.User
{
    public interface IUserCommand
    {
        Task<ResponseDto<LoginDto>> Login(LoginRequestDto  request);
        Task<ResponseDto> Add(UserAddRequestDto request);
    }
}
