using System.Security.Claims;
using System.Threading.Tasks;
using Commands.User;
using DataTransferObject.Enums;
using DataTransferObject.Queries.User;
using DataTransferObject.Shared;
using DataTransferObject.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queries.User;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserCommand _userCommand;
        private readonly IUserQuery _userQuery;
        public UserController(IUserCommand userCommand, IUserQuery userQuery)
        {
            _userCommand = userCommand;
            _userQuery = userQuery;
        }

        [Authorize(Roles = nameof(UserType.Manager))]
        [HttpGet, Route("index")]
        public async Task<ActionResult<ListResponseDto<UserListDto>>> Index()
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = long.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            var request = new ListRequestDto<UserListRequestDto>();
            request.Search = new UserListRequestDto
            {
                UserId = userId
            };
            var result = await _userQuery.List(request);
            return Ok(result);
        }
       
        [Authorize(Roles = nameof(UserType.Manager))]
        [HttpPost,Route("create")]
        public async Task<ActionResult<ResponseDto>> Create([FromBody]UserAddRequestDto request)
        {
            var result = await _userCommand.Add(request);
            return !result.Successful
                 ? NotFound(result)
                 : (ActionResult)Ok(result);
        }
    }
}
