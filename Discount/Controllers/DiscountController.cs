
using AutoMapper;
using Azure;
using Discount.Common;
using Discount.Entities;
using Discount.Entities.Dto;
using Discount.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;
        private ApiResponse _response;

        public DiscountController(IDiscountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _response = new ApiResponse();
        }

        [HttpGet(Name = "GetCouponList")]
        [ProducesResponseType(typeof(CouponDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCouponList()
        {
            try
            {
                _response.Result = _mapper.Map<List<CouponDto>>(await _repository.GetCouponList());
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpGet]
        [Route("{couponId:int}")]
        [ProducesResponseType(typeof(CouponDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCouponById(int couponId)
        {
            try
            {
                _response.Result = _mapper.Map<CouponDto>(await _repository.GetCouponById(couponId));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpGet]
        [Route("GetCouponByCode/{couponCode}")]
        [ProducesResponseType(typeof(CouponDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCouponByCode(string couponCode)
        {
            try
            {
                _response.Result = _mapper.Map<CouponDto>(await _repository.GetCouponByCode(couponCode));
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
        [ProducesResponseType(typeof(CouponDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateCoupon([FromBody] CouponDto couponDto)
        {
            _response.Result = _mapper.Map<CouponDto>(await _repository.CreateCoupon(_mapper.Map<Coupon>(couponDto)));
            return Ok(_response);
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(CouponDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateCoupon([FromBody] CouponDto couponDto)
        {
            //var coupondeails = await _repository.UpdateCoupon(_mapper.Map<Coupon>(couponDto));
            //var response = new ApiResponse<CouponDto>(true, _mapper.Map<CouponDto>(coupondeails), "Update coupon successfullly.");
            return Ok();
        }

        [HttpDelete]
        [Route("{couponId:int}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(CouponDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteCoupon(int couponId)
        {
            _response.Result = await _repository.DeleteCoupon(couponId);
            return Ok(_response);
        }
    }
}
