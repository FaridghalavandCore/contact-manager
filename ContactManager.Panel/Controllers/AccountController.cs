using DataTransferObject.Shared;
using DataTransferObject.User;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace ContactManager.Panel.Controllers
{
    [EnableCors("panel")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginRequestDto requestDto)
        {
            var client = new RestClient(_configuration["Api:Url"] + "Account/login");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new LoginRequestDto
            {
                Password = requestDto.Password,
                PhoneNumber = requestDto.PhoneNumber
            });
            var response = client.Execute<ResponseDto<LoginDto>>(request);
            HttpContext.Session.SetString("JWToken", response.Data.Response.Token);
            HttpContext.Session.SetString("Role", response.Data.Response.Role);

            return RedirectToAction("Index", "Home");
        }
    }
}
