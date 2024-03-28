using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Common;
using ProductAPI.Entities;
using ProductAPI.Entities.Dto;
using ProductAPI.Repositories.Interface;
using System.Net;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private ApiResponse _response;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _response = new ApiResponse();
        }

        [HttpGet(Name = "GetProductList")]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetProductList()
        {
            try
            {
                _response.Result = _mapper.Map<List<ProductDto>>(await _productRepository.GetProductList());
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpGet("{productId}", Name = "GetProductById")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetProductById(int productId)
        {
            try
            {
                _response.Result = _mapper.Map<ProductDto>(await _productRepository.GetProductById(productId));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            _response.Result = _mapper.Map<ProductDto>(await _productRepository.CreateProduct(_mapper.Map<ProductModel>(productDto)));
            return Ok(_response);
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            var productdeails = await _productRepository.UpdateProduct(_mapper.Map<ProductModel>(productDto));
            _response.Result = _mapper.Map<ProductDto>(productdeails);
            _response.Message = "Successfully update the product.";
            return Ok(_response);
        }

        [HttpDelete]
        [Route("{productId:int}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            _response.Result = await _productRepository.DeleteProduct(productId);
            return Ok(_response);
        }

    }
}
