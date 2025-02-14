using Microsoft.AspNetCore.Mvc;
using StoreAPI.Common.Dtos;
using StoreAPI.Core.Interfaces;
using StoreAPI.Infraestructure.EntityFramework.UnitOfWork;

    namespace StoreAPI.WebApi.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
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

        // api/featured
        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedProducts()
        {
            var featuredProducts = await _productService.GetFeaturedProductsAsync();
            return Ok(featuredProducts);
        }

        // api/product-detail/:productId
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

        // api/product-detail/:Id
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var allProducts = await _productService.GetAllProductsAsync();
            if (allProducts == null)
            {
                return NotFound("No products has been found.");

            } 
            return Ok(allProducts);
        }

        // api/GetWithKeysetPagination
        [HttpGet("GetWithKeysetPagination")]
        public async Task<IActionResult> GetWithKeysetPagination(int reference = 0, int pageSize = 10)
        {
            if (pageSize <= 0)
                return BadRequest($"{nameof(pageSize)} size must be greater than 0.");

            var pagedProductsDto = await _productService.GetWithKeysetPagination(reference, pageSize);

            return Ok(pagedProductsDto);
        }

        // api/delete/:productId
        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> Delete(int productId) 
        { 
            if(productId <= 0) { return BadRequest("Product Id is smaller than 0 or null"); }

            var existentProduct = await _productService.GetProductAsync(productId);
            if (existentProduct == null)
            {
                return NotFound();
            }

            try
            {
                await _productService.DeleteProductAsync(productId);
                return Ok(new { message = "Product deleted" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // api/post/
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductDto model)
        {
            try
            {
                var newProduct = await _productService.Post(model);

                if (newProduct != null)
                {
                    return Ok(new { message = $"New product created", product = newProduct });
                }

                return BadRequest(new { message = "Failed to create product" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while creating product: {ex.Message}");
                return StatusCode(500, new { message = "Unexpected exception creating product", error = ex.Message });
            }
        }

    }
}   
