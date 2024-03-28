
using static Web.Utility.StaticDetail;

namespace Web.Models
{
    public class RequestDto
    {
        public MethodEnum ApiType { get; set; } = MethodEnum.GET;
        public string? Url { get; set; }
        public object? Data { get; set; }
    }
}
