﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_AE_WebShop.Data.Entities;
using Project_AE_WebShop.Services;

namespace Project_AE_WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ProductService _productService;

        public ShopController(ProductService productService) 
        {
            _productService = productService;
        }

        [HttpGet("get-products")]
        public async Task<IActionResult> Products()
        {
            return Ok(await _productService.GetProductsAsync());
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> Add(Product product)
        {
            if(await _productService.AddProductAsync(product)) 
            {
                return Ok(product);
            }

            return BadRequest();
        }

        [HttpPost("update-basket")]
        public async Task<IActionResult> UpdateBasket([FromQuery] int basketId, [FromBody] List<Orderdto> ordersdto)
        {
            var orders = new List<Order>();
            var dbOrders = await _productService.GetOrdersAsync();
            foreach (var orderdto in ordersdto)
            {
                var order = new Order
                {
                    BasketId = basketId,
                    Price = orderdto.Price,
                    Quantity = orderdto.Quantity,
                    Product = orderdto.Product,
                    ProductId = orderdto.ProductId,
                    Id = dbOrders.FirstOrDefault(o => o.ProductId == orderdto.ProductId)?.Id ?? 0,
                };
                orders.Add(order);
                
            }
            if (await _productService.UpdateBasketAsync(basketId, orders))
                return Ok();

            return BadRequest();
        }

        [HttpPost("add-basket")]
        public async Task<IActionResult> AddBasket(Basket basket)
        {
            if (await _productService.AddBasketAsync(basket))
                return Ok(basket);

            return BadRequest();
        }

        [HttpGet("get-basket/{userId}")]
        public async Task<IActionResult> GetBasket(Guid userId)
        {
            var basket = await _productService.GetBasketAsync(userId);

            if(basket != null)
                return Ok(basket);

            return BadRequest();
        }

        [HttpDelete("empty-basket/{basketId}")]
        public async Task<IActionResult> EmptyBasket(int  basketId)
        {
            if (await _productService.DeleteBasketAsync(basketId))
                return Ok(basketId);

            return BadRequest();
        }

    }
}
