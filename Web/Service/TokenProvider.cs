using Web.Service.IService;
using Web.Utility;

namespace Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenProvider(IHttpContextAccessor httpContextAccessor){
            _httpContextAccessor = httpContextAccessor;
        }

        public void ClearToken()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(StaticDetail.TokenCookies);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(StaticDetail.TokenCookies, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            httpContext.Response.Cookies.Append(StaticDetail.TokenCookies, token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // If using HTTPS
                SameSite = SameSiteMode.Strict, // Or adjust it based on your requirements
                Expires = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30)) // Example: Cookie expires in 30 days
            });
        }
    }
}
