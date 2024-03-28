namespace Web.Utility
{
    public class StaticDetail
    {
        public static string? DiscountApiBase { get; set; }
        public static string? AuthApiBase { get; set; }
        public static string? ProductApiBase { get; set; }
        public static string? ShoppingApiBase { get; set; }

        public const string? RoleAdmin = "ADMIN";
        public const string? RoleCustomer = "CUSTOMER";
        public const string? TokenCookies = "JWTToken";


        public enum MethodEnum
        {
            GET,
            POST,
            PUT,
            DELETE,
        }
    }    
}
