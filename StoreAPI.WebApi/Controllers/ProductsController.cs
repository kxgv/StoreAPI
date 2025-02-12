using Microsoft.AspNetCore.Mvc;
using StoreAPI.Common.Dtos;
using StoreAPI.Core.Interfaces;
using StoreAPI.Infraestructure.EntityFramework.UnitOfWork;

    namespace StoreAPI.WebApi.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class ProductsController : ControllerBase
        {
            private readonly ILogger<ProductsController> _logger;
            private readonly IProductService _productService;

            public ProductsController(
                ILogger<ProductsController> logger, IProductService productService) 
            { 
                _logger = logger;
                _productService = productService;
            }

        //api/featured
        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedProducts()
        {
            var featuredProducts = await _productService.GetFeaturedProductsAsync();
            return Ok(featuredProducts);
        }

        // /product-detail/:Id
        [HttpGet("product-detail/{productId}")]
        public async Task<IActionResult> GetProductDetail(int productId)
        {
            var detailedProduct = await _productService.GetProductDetailAsync(productId);
            if(detailedProduct == null)
            {
                return NotFound($" Product with Id {productId} not found.");
            }
            return Ok(detailedProduct);
        }   

        // api/products
        [HttpGet("GetWithKeysetPagination")]
        public async Task<IActionResult> GetWithKeysetPagination(int reference = 0, int pageSize = 10)
        {
            if (pageSize <= 0)
                return BadRequest($"{nameof(pageSize)} size must be greater than 0.");

            var pagedProductsDto = await _productService.GetWithKeysetPagination(reference, pageSize);

            return Ok(pagedProductsDto);
        }

        // api/products/delete/{productId}
        [HttpPost("productId")]
        public async Task<IActionResult> Post(int productId, ProductDto model)
        {
            if(productId <= 0)
            {
                return BadRequest(new {message = "Product Id is smaller than 0"});
            }

            //var existingProduct = await _productService.GetProductById(productId);

            //if(existingProduct != null)
            //{
            //    return Conflict(new {message = $"Product with Id {productId} already exists"});
            //}

            try
            {

                var testProduct = new ProductDto
                {
                    Id = 0, // Si es autogenerado en la BD, se ignora
                    Guid = Guid.NewGuid(), // Genera un GUID único
                    Name = "Producto de Prueba",
                    Color = "Rojo",
                    Price = 19.99,
                    Size = "M",
                    Description = "Este es un producto de prueba para la base de datos."
                };

                var newProduct = await _productService.Post(0, testProduct);
                return Ok($"New product creataed with Id {newProduct.Id}");
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message, new { message = $"Error creating product {productId}" });
                return StatusCode(500, new {message = "Unexpected expection creating product" });
            }
        }

    }
}   
