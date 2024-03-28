using Discount.Entities.Dto;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProductAPI.Common;
using RestSharp;
using ShoppingCartAPI.Common;
using ShoppingCartAPI.Repositories.Interface;

namespace ShoppingCartAPI.Repositories
{
    public class DiscountService : IDiscountService
    {
        private readonly IOptions<AppSetting> _options;
        private readonly RestClient _client;

        public DiscountService(IOptions<AppSetting> options)
        {
            _options = options;
            _client = new RestClient(_options.Value.DiscountAPI);
        }
        public async Task<CouponDto> GetCouponList(string couponCode)
        {       
            RestRequest restRequest = new RestRequest($"api/coupon/GetCouponByCode/{couponCode}", Method.Get);
            var result = JsonConvert.DeserializeObject<ApiResponse>((await _client.ExecuteAsync(restRequest)).Content);
            if(result != null && result.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(result.Result));
            }

            return new CouponDto();
        }
    }
}
