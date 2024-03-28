using System.ComponentModel.DataAnnotations;

namespace Coupon.Entities
{
    public class CouponModel
    {
        [Key]
        public int CouponId { get; set; }

        [Required]
        public string? CouponCode { get; set; }

        [Required]
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}