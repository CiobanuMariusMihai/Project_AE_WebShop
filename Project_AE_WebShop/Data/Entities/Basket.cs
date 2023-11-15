using Project_AE_WebShop.Models;

namespace Project_AE_WebShop.Data.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public double Price { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

    }
}
