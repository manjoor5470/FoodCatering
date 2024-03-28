using Web.Models;

namespace Web.Service.IService
{
    public interface IShoppingService
    {
        Task<ResponseDto?> CreateUpSertAsync(CartDto cartDto);
        Task<ResponseDto?> CartDetailsAsync(string userId);
        Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);
        Task<ResponseDto?> RemoveCartAsync(int cartDetailId);
        Task<ResponseDto?> RemoveCouponAsync(CartDto cartDto);


    }
}
