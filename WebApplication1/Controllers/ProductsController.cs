using BBL.DTOModels;
using BBL.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using static BBL.ServiceInterfaces.IProduct;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct product;

        public ProductsController(IProduct product)
        {
            this.product = product;
        }

        [HttpGet]
        public IActionResult GetProducts(bool? desc, string? filtrByName = null, string? filtrByGroup = null,
    int? filtrByGroupId = null, FiltrBy? filtrBy = null, bool? showNonActive = null)
        {
            var p = product.GetProducts(desc, filtrByName, filtrByGroup, filtrByGroupId, filtrBy, showNonActive);
            if (p == null)
                return NotFound();
            return Ok(p);
        }
        [HttpDelete("{productId}")]
        public bool RemoveProduct(int? productId)
        {
            return productId.HasValue ? product.RemoveProduct(productId.Value) : false;
        }
        [HttpPut("{productId}/ChangeActivity")]
        public bool ChangeActivityOfProduct(int? productId, bool state) 
        {
            return productId.HasValue ? product.ChangeActivityOfProduct(productId.Value, state) : false;
        }
        [HttpPost]
        public bool AddNewProduct(string name, double price, int groupId) 
        {
            return  product.AddNewProduct(name,price,groupId);
        }
    }
}
