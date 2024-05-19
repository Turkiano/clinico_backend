
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using sda_onsite_2_csharp_backend_teamwork.src.Abstractions;
using sda_onsite_2_csharp_backend_teamwork.src.Databases;
using sda_onsite_2_csharp_backend_teamwork.src.DTOs;
using sda_onsite_2_csharp_backend_teamwork.src.Entities;

namespace sda_onsite_2_csharp_backend_teamwork.src.Controllers

{
    public class ProductController : CostumeController
    {

        private readonly IProductService _productService; //this to inject the service methods into the controller

        public ProductController(IProductService productService) // this is the constructor dependency injection
        {
            _productService = productService;
        }
        // products?limit=7&page=2&search="bmw"

        [HttpGet]
        public IEnumerable<Product> FindAll([FromQuery] int limit = 5, [FromQuery] int page = 1,
                                            [FromQuery] string? search = null)
        {

            Console.WriteLine($"search {search}");

            return _productService.FindAll(limit, page, search); // this to run the method to get all the products
        }


        [HttpPost] //to use this method, import AspNetCore
        public Product CreateOne([FromBody] ProductCreateDto productCreateDto) //this is the body example to send data
        {
            return _productService.CreateOne(productCreateDto);//this is how we talk to service
        }

        [HttpDelete("{productId}")]
        public Product? DeleteProduct(Guid productId)
        {
            return _productService.DeleteProduct(productId);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductReadDto> FindOne(Guid productId)
        {
            ProductReadDto? product = _productService.FindOne(productId);
            if (product is null) return NotFound();

            return Ok(product);
        }

    }
}