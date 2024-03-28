using AutoMapper;
using Discount.Entities.Dto;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Repositories.Interface;
using ShoppingCartAPI.Common;
using ShoppingCartAPI.Entities;
using ShoppingCartAPI.Entities.Dto;
using ShoppingCartAPI.Repositories.Interface;

namespace ProductAPI.Repositories
{
    public class ShoppingCartRepository: IShoppingCartRepository
    {
        private readonly ShoppingDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;

        public ShoppingCartRepository(ShoppingDbContext dbContext, IMapper mapper, IProductService productService, IDiscountService discountService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _productService = productService;
            _discountService = discountService;
        }        

        public async Task<Cart> CartDetails(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _dbContext.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId)
            };
            cart.CartDetails = _dbContext.CartDetail.Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId);
            List<ProductDto> productList = await _productService.ProductList(userId);
            double sum = 0;
            foreach (var item in cart.CartDetails)
            {
                item.Product = productList.FirstOrDefault(u => u.ProductId == item.ProductId);
                sum += Math.Round(Math.Abs(item.Count * item.Product.Price), 2);
            }

            cart.CartHeader.CartTotal = sum;

            if(!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
            {
                CouponDto couponDto = await _discountService.GetCouponList(cart.CartHeader.CouponCode);
                cart.CartHeader.Discount = couponDto.DiscountAmount;
                cart.CartHeader.CartTotal -= couponDto.DiscountAmount;
            }
            return cart;
        }

        public async Task<Cart?> CreateUpSert(Cart cart)
        {
            var cartHeaderFromDb = await _dbContext.CartHeader.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);
            if(cartHeaderFromDb == null)
            {
                // create cart header and cart details
                _dbContext.CartHeader.Add(_mapper.Map<CartHeader>(cart.CartHeader));
                await _dbContext.SaveChangesAsync();

                cart.CartDetails.First().CartHeaderId = cart.CartHeader.CartHeaderId;
                _dbContext.CartDetail.Add(_mapper.Map<CartDetail>(cart.CartDetails.First()));
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                // if header is not null
                // check if deatils has same product

                var cartDetailFromDb = await _dbContext.CartDetail.AsNoTracking().FirstOrDefaultAsync(u => u.ProductId == cart.CartDetails.First().ProductId
                                             && u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                if(cartDetailFromDb == null)
                {
                    // create cart details
                    cart.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    _dbContext.CartDetail.Add(_mapper.Map<CartDetail>(cart.CartDetails.First()));
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    // update count in cart details
                    cart.CartDetails.First().Count += cartDetailFromDb.Count;
                    cart.CartDetails.First().CartHeaderId = cartDetailFromDb.CartHeaderId;
                    cart.CartDetails.First().CartDetailsId = cartDetailFromDb.CartDetailsId;
                    _dbContext.CartDetail.Update(_mapper.Map<CartDetail>(cart.CartDetails.First()));
                    await _dbContext.SaveChangesAsync();
                }
            }
            return cart;
        }

        public async Task<bool?> RemoveCart(int cartDetailId)
        {
            CartDetail cartDetail = await _dbContext.CartDetail.FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailId);
            if(cartDetail == null)
            {
                int totalCountCartItem = _dbContext.CartDetail.Where(u => u.CartHeaderId == cartDetailId).Count();
                _dbContext.CartDetail.Remove(cartDetail);
                if (totalCountCartItem == 1)
                {
                    var cartHeaderRemove = await _dbContext.CartHeader.FirstOrDefaultAsync(u => u.CartHeaderId == cartDetail.CartHeaderId);
                    _dbContext.CartHeader.Remove(cartHeaderRemove);
                }
                await _dbContext.SaveChangesAsync();
                return true;
            }            
            return false;
        }

        public async Task<bool?> RemoveCoupon(Cart cart)
        {
            if(cart != null)
            {
                var cartdetail = await _dbContext.CartHeader.FirstAsync(u => u.UserId == cart.CartHeader.UserId);
                if(cartdetail != null)
                {
                    cartdetail.CouponCode = string.Empty;
                    _dbContext.CartHeader.Update(cartdetail);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }                
            }
            return false;
        }

        public async Task<bool?> ApplyCoupon(Cart cart)
        {
            if (cart != null)
            {
                var cartdetail = await _dbContext.CartHeader.FirstAsync(u => u.UserId == cart.CartHeader.UserId);
                if (cartdetail != null)
                {
                    cartdetail.CouponCode = cart.CartHeader.CouponCode;
                    _dbContext.CartHeader.Update(cartdetail);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
