using System.Threading.Tasks;
using DataTransferObject.Commands.Contact;
using DataTransferObject.Queries.Contact;
using DataTransferObject.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;

namespace ContactManager.Panel.Controllers
{
    public class ContactController : Controller
    {
        private readonly IConfiguration _configuration;
        public ContactController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var client = new RestClient(_configuration["Api:Url"] + "contact/index");
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            var token = HttpContext.Session.GetString("JWToken");
            client.Authenticator = new JwtAuthenticator(token);
            var response = await client.ExecuteAsync<ListResponseDto<ContactListDto>>(request);
            return View(response.Data.List);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddContactRequestDto requestDto)
        {
            var client = new RestClient(_configuration["Api:Url"] + "contact/create");
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
