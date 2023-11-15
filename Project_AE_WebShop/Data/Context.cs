using Project_AE_WebShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_AE_WebShop.Data.Entities;

namespace Project_AE_WebShop.Data
{
    public class Context : IdentityDbContext<User>
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Basket> Baskets { get; set; }
    }
}
