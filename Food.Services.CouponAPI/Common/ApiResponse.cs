namespace Food.Services.CouponAPI.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public ApiResponse(bool success, T data, string errorMessage = null)
        {
            Success = success;
            Data = data;
            ErrorMessage = errorMessage;
        }
    }
}
