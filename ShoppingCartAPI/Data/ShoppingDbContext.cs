using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.Entities;
using ShoppingCartAPI.Entities.Dto;

namespace ProductAPI.Data
{
    public class ShoppingDbContext: DbContext
    {
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options) : base(options)
        {
        }
        public DbSet<CartDetail> CartDetail { get; set; }
        public DbSet<CartHeader> CartHeader { get; set; }
    }
}
