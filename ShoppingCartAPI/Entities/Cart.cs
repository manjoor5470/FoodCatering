using ShoppingCartAPI.Entities.Dto;

namespace ShoppingCartAPI.Entities
{ 
    public class Cart
    {
        public CartHeader? CartHeader { get; set; }
        public IEnumerable<CartDetail>? CartDetails { get; set; }
    }
}
