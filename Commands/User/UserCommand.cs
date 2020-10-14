using DataTransferObject.Shared;
using DataTransferObject.User;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Extentions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Commands.User
{
    public class UserCommand : IUserCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public UserCommand(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<ResponseDto> Add(UserAddRequestDto request)
        {
            request.Password = (request.Password + request.PhoneNumber).EncryptSha256();
            var vaidateResult = await _unitOfWork.UserRepository.UserAddValidation(new UserAddValidationRequestDto
            {
                Password = request.Password,
                Phonenumber = request.PhoneNumber
            });
            if (!vaidateResult.Successful)
                return new ResponseDto { Successful = vaidateResult.Successful, Title = vaidateResult.Title };

            
            var addUserResult = await _unitOfWork.UserRepository.Add(request);

            return addUserResult;
        }

        public async Task<ResponseDto<LoginDto>> Login(LoginRequestDto request)
        {
            request.Password += request.PhoneNumber;
            var user = await _unitOfWork.UserRepository.FindUserWithPhoneNumberPassword(new FindUserWithPhoneNumberPasswordRequestDto
            {
                Password = request.Password.EncryptSha256(),
                PhoneNumber = request.PhoneNumber
            });

            if (!user.Successful)
                return new ResponseDto<LoginDto>
                {
                    Successful = user.Successful,
                    Title = user.Title
                };

            var token = CreateToken(new CreateTokenRequestDto
            {
                FullName = user.Response.FirstName + " " + user.Response.LastName,
                Role = user.Response.UserType.ToString(),
                Id = user.Response.Id
            });

            if (!token.Successful)
                return new ResponseDto<LoginDto>
                {
                    Successful = token.Successful,
                    Title = token.Title,
                };


            return new ResponseDto<LoginDto>
            {
                Response = new LoginDto
                {
                    Id = user.Response.Id,
                    FristName = user.Response.FirstName,
                    LastName = user.Response.LastName,
                    Token = token.Response.Token,
                    TokenExpiry = token.Response.TokenExpiry,
                    Role = user.Response.UserType.ToString()
                },
                Successful = true,
                Title = "validate user successful"
            };
        }
        private ResponseDto<CreateTokenDto> CreateToken(CreateTokenRequestDto request)
        {
            try
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,request.Id.ToString()),
                new Claim("FullName",request.FullName),
                new Claim(ClaimTypes.Role,request.Role),
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["BearerTokens:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expire = int.Parse(_configuration["BearerTokens:AccessTokenExpirationMinutes"]);
                var now = DateTime.UtcNow;

                var token = new JwtSecurityToken(_configuration["BearerTokens:Issuer"],
                    _configuration["BearerTokens:Audience"], claims, now,
                    now.AddMinutes(expire), credentials);

                return new ResponseDto<CreateTokenDto>
                {
                    Response = new CreateTokenDto
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Claims = claims,
                        TokenExpiry = expire
                    },
                    Successful = true,
                    Title = "token created"
                };

            }
            catch
            {
                return new ResponseDto<CreateTokenDto>
                {
                    Successful = false,
                    Title = "create token failed"
                };
            }
        }
    }
}
