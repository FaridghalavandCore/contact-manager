using System.Threading.Tasks;
using DataTransferObject.Queries.User;
using DataTransferObject.Shared;
using DataTransferObject.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;

namespace ContactManager.Panel.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index(ListRequestDto<UserListDto> requestDto)
        {
            var client = new RestClient(_configuration["Api:Url"] + "user/index");
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            var token = HttpContext.Session.GetString("JWToken");
            client.Authenticator = new JwtAuthenticator(token);
            var response = await client.ExecuteAsync<ListResponseDto<UserListDto>>(request);
            return View(response.Data.List);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserAddRequestDto requestDto)
        {
            var client = new RestClient(_configuration["Api:Url"] + "user/create");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            var token = HttpContext.Session.GetString("JWToken");
            client.Authenticator = new JwtAuthenticator(token);
            request.AddHeader("contentType", "application/json; charset=utf-8");
            request.AddJsonBody(requestDto);
            var response = await client.ExecuteAsync<ResponseDto>(request);
            return Json(response.Data);
        }
    }
}
