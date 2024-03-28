using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Repositories.Interface;
using ShoppingCartAPI.Common;
using ShoppingCartAPI.Entities;
using ShoppingCartAPI.Entities.Dto;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;
        private readonly ApiResponse _apiResponse;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _apiResponse = new ApiResponse();
        }

        [HttpPost("CreateUpSert")]
        public async Task<IActionResult> CreateUpSert(Cart cartDto)
        {
            try
            {
                _apiResponse.Result = _mapper.Map<CartDto>(await _shoppingCartRepository.CreateUpSert(cartDto));
            }
            catch (Exception ex)
            {

                _apiResponse.Result = null;
                _apiResponse.IsSuccess = false;
                _apiResponse.Message = ex.Message.ToString();
            }
            return Ok(_apiResponse);
        }

        [HttpPost("RemoveCart")]
        public async Task<IActionResult> RemoveCart([FromBody] int cartDetailId)
        {
            try
            {
                _apiResponse.Result = await _shoppingCartRepository.RemoveCart(cartDetailId);
            }
            catch (Exception ex)
            {
                _apiResponse.Result = null;
                _apiResponse.IsSuccess = false;
                _apiResponse.Message = ex.Message.ToString();
            }
            return Ok(_apiResponse);
        }

        [HttpGet("CartDetails/{userId}")]
        public async Task<IActionResult> CartDetails(string userId)
        {
            try
            {
                _apiResponse.Result = _mapper.Map<CartDto>(await _shoppingCartRepository.CartDetails(userId));
            }
            catch (Exception ex)
            {
                _apiResponse.Result = null;
                _apiResponse.IsSuccess = false;
                _apiResponse.Message = ex.Message.ToString();
            }
            return Ok(_apiResponse);
        }

        [HttpPost("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                _apiResponse.Result = await _shoppingCartRepository.ApplyCoupon(_mapper.Map<Cart>(cartDto));
            }
            catch (Exception ex)
            {
                _apiResponse.Result = null;
                _apiResponse.IsSuccess = false;
                _apiResponse.Message = ex.Message.ToString();
            }
            return Ok(_apiResponse);
        }

        [HttpPost("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                _apiResponse.Result = await _shoppingCartRepository.RemoveCoupon(_mapper.Map<Cart>(cartDto));
            }
            catch (Exception ex)
            {
                _apiResponse.Result = null;
                _apiResponse.IsSuccess = false;
                _apiResponse.Message = ex.Message.ToString();
            }
            return Ok(_apiResponse);
        }
    }
}
