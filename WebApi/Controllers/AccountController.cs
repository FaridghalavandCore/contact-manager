using System.Threading.Tasks;
using Commands.User;
using DataTransferObject.Shared;
using DataTransferObject.User;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserCommand _userCommand;
        public AccountController(IUserCommand userCommand)
        {
            _userCommand = userCommand;
        }
        [HttpPost, Route("login")]
        public async Task<ActionResult<ResponseDto<LoginDto>>> Login(LoginRequestDto request)
        {
            var result = await _userCommand.Login(request);
            var IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            return !result.Successful
                 ? NotFound(result)
                 : (ActionResult)Ok(result);
        }
    }
}
