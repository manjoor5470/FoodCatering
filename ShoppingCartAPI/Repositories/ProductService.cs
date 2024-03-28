using Microsoft.Extensions.Options;
using ProductAPI.Common;
using RestSharp;
using ShoppingCartAPI.Common;
using ShoppingCartAPI.Entities.Dto;
using ShoppingCartAPI.Repositories.Interface;
using System.Text.Json.Serialization;

using Newtonsoft.Json;

namespace ShoppingCartAPI.Repositories
{
    public class ProductService : IProductService
    {
        private readonly IOptions<AppSetting> _options;
        private readonly RestClient _client;

        public ProductService(IOptions<AppSetting> options)
        {
            _options = options;
            _client = new RestClient(_options.Value.ProductAPI);
        }
        public async Task<List<ProductDto>> ProductList(string ProductId)
        {
            var request = new RestRequest("api/Product", Method.Get);
            var result = await _client.ExecuteAsync(request);
            var response = JsonConvert.DeserializeObject<ApiResponse>(result.Content);
            return JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
        }
    }
}
