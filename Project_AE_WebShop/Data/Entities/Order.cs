﻿using System.Text.Json.Serialization;

namespace Project_AE_WebShop.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        [JsonIgnore]
        public int BasketId { get; set; }
        [JsonIgnore]
        public Basket Basket { get; set; }
    }
    public class Orderdto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        [JsonIgnore]
        public int BasketId { get; set; }
        
        
    }
}

