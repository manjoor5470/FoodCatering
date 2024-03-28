

using ShoppingCartAPI.Common;
using ShoppingCartAPI.Entities;
using ShoppingCartAPI.Entities.Dto;

namespace ProductAPI.Repositories.Interface
{
    public interface IShoppingCartRepository
    {
        Task<Cart?> CreateUpSert(Cart cart);

        Task<bool?> RemoveCart(int cartDetailId);

        Task<Cart> CartDetails(string userId);

        Task<bool?> ApplyCoupon(Cart cart);

        Task<bool?> RemoveCoupon(Cart cart);
    }
}
