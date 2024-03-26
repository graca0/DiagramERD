using BBL.DTOModels;
using BBL.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasket basket;

        public BasketsController(IBasket basket)
        {
            this.basket = basket;
        }
        [HttpPost("AddProductToBasket")]
        public bool AddProductToBasket(BasketRequest basket)
        {
            return this.basket.AddProductToBasket(basket);
        }
        [HttpPost("ChangeBasketAmount")]
        public bool ChangeBasketAmount(BasketRequest basket)
        {
            return this.basket.ChangeBasketAmount(basket);
        }
        [HttpDelete("RemoveProductBasket")]
        public bool RemoveProductBasket(int productId, int userId)
        {
            return this.basket.RemoveProductBasket(productId, userId);
        }
        [HttpPost("GenerateOrder")]
        public bool GenerateOrder(int userId)
        {
            return this.basket.GenerateOrder(userId);
        }
        [HttpPost("Payment")]
        public bool Payment(int userId, int payment)
        {
            return basket.Payment(userId, payment);
        }
    }
}
