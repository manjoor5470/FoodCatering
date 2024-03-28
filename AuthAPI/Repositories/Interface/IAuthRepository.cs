using AuthAPI.Entities.Dto;

namespace AuthAPI.Repositories.Interface
{
    public interface IAuthRepository
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
