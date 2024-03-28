using AuthAPI.Data;
using AuthAPI.Entities.Dto;
using AuthAPI.Models;
using AuthAPI.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public readonly AuthDbContext _dbContext;

        public readonly UserManager<ApplicationUser> _userManager;

        public readonly RoleManager<IdentityRole> _roleManager;

        public readonly ITokenRepository _tokenRepository;

        public AuthRepository(RoleManager<IdentityRole> roleManager, AuthDbContext dbContext, UserManager<ApplicationUser> userManager, ITokenRepository tokenRepository)
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        public TokenRepository TokenRepository { get; }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if(!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    // create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;

        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            var roles = await _userManager.GetRolesAsync(user);

            // if user found then generate token
            var token = _tokenRepository.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };
            return new LoginResponseDto() { User = userDto, Token = token };
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToLower(),
                PhoneNumber = registrationRequestDto.PhoneNumber,
                Name = registrationRequestDto.Name
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                return result.Succeeded ? "" : result.Errors.FirstOrDefault().Description;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
