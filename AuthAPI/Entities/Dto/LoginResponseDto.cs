namespace AuthAPI.Entities.Dto
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string? Token { get; set; }
    }
}