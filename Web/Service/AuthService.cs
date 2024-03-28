using Web.Models;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registerRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.POST,
                Data = registerRequestDto,
                Url = StaticDetail.AuthApiBase + "/api/auth/assignrole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.POST,
                Data = loginRequestDto,
                Url = StaticDetail.AuthApiBase + "/api/auth/login"
            }, isBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.POST,
                Data = registrationRequestDto,
                Url = StaticDetail.AuthApiBase + "/api/auth/register"
            }, isBearer: false);
        }
    }
}
