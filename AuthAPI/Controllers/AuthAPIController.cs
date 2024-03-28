using AuthAPI.Entities.Dto;
using AuthAPI.Repositories.Interface;
using Discount.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        protected ApiResponse _apiResponse; 

        public AuthAPIController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            _apiResponse = new ApiResponse();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var errroMessgae = await _authRepository.Register(registrationRequestDto);
            if(!string.IsNullOrEmpty(errroMessgae))
            {
                _apiResponse.Message = errroMessgae;
                _apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto) 
        {
            var loginResponse = await _authRepository.Login(loginRequestDto);
            if(loginResponse != null && loginResponse.User == null)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.Message = "Username and password incorrect";
                return BadRequest(_apiResponse);
            }
            _apiResponse.Result = loginResponse;
            return Ok(_apiResponse);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var roleResponse = await _authRepository.AssignRole(registrationRequestDto.Email, registrationRequestDto.Role.ToUpper());
            if (!roleResponse)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.Message = "Error Encounterd";
                return BadRequest(_apiResponse);
            }
            return Ok(_apiResponse);
        }
    }
}
