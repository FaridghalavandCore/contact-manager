using System.Security.Claims;
using System.Threading.Tasks;
using Commands.Command;
using DataTransferObject.Commands.Contact;
using DataTransferObject.Enums;
using DataTransferObject.Queries.Contact;
using DataTransferObject.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queries.Contact;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactCommand _contactCommand;
        private readonly IContactQuery _contactQuery;
        public ContactController(IContactCommand contactCommand, IContactQuery contactQuery)
        {
            _contactCommand = contactCommand;
            _contactQuery = contactQuery;
        }
        [Authorize(Roles = nameof(UserType.User))]
        [HttpGet, Route("index")]
        public async Task<ActionResult<ListResponseDto<ContactListDto>>> Index()
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = long.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);

            var request = new ListRequestDto<ContactListRequestDto>
            {
                Search = new ContactListRequestDto
                {
                    UserId = userId
                }
            };
            var result = await _contactQuery.List(request);
            return Ok(result);
        }
        [Authorize(Roles = nameof(UserType.User))]
        [HttpPost, Route("create")]
        public async Task<ActionResult<ResponseDto>> Create(AddContactRequestDto request)
        {

            var identity = User.Identity as ClaimsIdentity;
            request.UserId = long.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _contactCommand.Add(request);
            return !result.Successful
                ? NotFound(result)
                : (ActionResult)Ok(result);
        }
    }
}
