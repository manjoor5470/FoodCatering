using AutoMapper;
using Food.Services.CouponAPI.Common;
using Food.Services.CouponAPI.Data;
using Food.Services.CouponAPI.Models;
using Food.Services.CouponAPI.Models.Dto;
using Food.Services.CouponAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Food.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly ICouponRepository _repository;
        private readonly IMapper _mapper;

        public DiscountController(ICouponRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetCouponList")]
        [ProducesResponseType(typeof(CouponDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCouponList()
        { 
            var response = new ApiResponse<List<CouponDto>>(true, _mapper.Map<List<CouponDto>>(await _repository.GetCouponList()));
            return Ok(response);          
        }

        [HttpGet("{couponId}" ,Name = "GetCouponById")]
        [ProducesResponseType(typeof(CouponDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCouponById(int couponId)
        {
            var response = new ApiResponse<CouponDto>(true, _mapper.Map<CouponDto>(await _repository.GetCouponById(couponId)));
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CouponDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddCoupon([FromBody] CouponDto couponDto)
        {
            var response = new ApiResponse<CouponDto>(true, _mapper.Map<CouponDto>(await _repository.AddCoupon(_mapper.Map<Coupon>(couponDto))));
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CouponDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateCoupon([FromBody] CouponDto couponDto)
        {
            var coupondeails = await _repository.UpdateCoupon(_mapper.Map<Coupon>(couponDto));
            var response = new ApiResponse<CouponDto>(true, _mapper.Map<CouponDto>(coupondeails), "Update coupon successfullly.");
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(CouponDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteCoupon(int couponId)
        {
            var isDeleted = await _repository.DeleteCoupon(couponId);
            var response = new ApiResponse<bool>(true, isDeleted, "Record deleted successfully.");
            return Ok(response);
        }
    }
}
