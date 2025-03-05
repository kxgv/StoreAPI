using Microsoft.AspNetCore.Mvc;
using StoreAPI.Common.Dtos;
using StoreAPI.Core.Interfaces;
using StoreAPI.Infraestructure.EntityFramework.Daos;
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

        // /featured
        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedProducts()
        {
            _logger.LogDebug("{MethodName}", nameof(GetFeaturedProducts));
            var featuredProducts = await _productService.GetFeaturedProductsAsync();
            return Ok(featuredProducts);
        }

        // /product-detail/:productId
        [HttpGet("product-detail/{productId}")]
        public async Task<IActionResult> GetProductDetail(int productId)
        {
            _logger.LogDebug("{MethodName}", nameof(GetProductDetail));
            if (productId <= 0)
            {
                return BadRequest("Product Id is invalid");
            }
            var detailedProduct = await _productService.GetProductDetailAsync(productId);
            return Ok(detailedProduct);
        }
        
        // /product-edit/:productId
        [HttpGet("product-edit/{productId}")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            _logger.LogDebug("{MethodName}", nameof(GetProduct));
            if (productId <= 0)
            {
                return BadRequest("Product Id is invalid");
            }
            var detailedProduct = await _productService.GetProductDetailAsync(productId);
            return Ok(detailedProduct);
        }

        // /product-detail/:Id
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var allProducts = await _productService.GetAllProductsAsync();
            return Ok(allProducts);
        }

        // /GetWithKeysetPagination
        [HttpGet("GetWithKeysetPagination")]
        public async Task<IActionResult> GetWithKeysetPagination(int reference = 0, int pageSize = 10)
        {
            if (pageSize <= 0)
                return BadRequest($"{nameof(pageSize)} size must be greater than 0.");

            var pagedProductsDto = await _productService.GetWithKeysetPagination(reference, pageSize);

            return Ok(pagedProductsDto);
        }

        // /delete/:productId
        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> Delete(int productId) 
        { 
            _logger.LogDebug("{MethodName}", nameof(Delete));
            if(productId <= 0) { return BadRequest("Product Id is smaller than 0 or null"); }
            
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
        
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductDto model)
        {
            try
            {
                _logger.LogDebug("{MethodName}", nameof(Post));
                var newProduct = await _productService.Post(model);
                return Ok(new { message = $"New product created", product = newProduct });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while creating product: {ex.Message}");
                return StatusCode(500, new { message = "Unexpected exception creating product", error = ex.Message });
            }
        }
        
        // /update/{productId}
        [HttpPut("update/{productId}")]
        public async Task<IActionResult> Put(int productId, CreateProductDto model)
        {
            try
            {
                _logger.LogDebug("{MethodName}", nameof(Put));
                var updatedProduct = await _productService.Put(productId, model);
                return Ok(new { message = $"Product with ID {productId} updated", product = updatedProduct });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating product ID {productId}: {ex.Message}");
                return StatusCode(500, new { message = "Unexpected error updating product", error = ex.Message });
            }
        }

        // /getPagedProducts
        [HttpGet("getPagedProducts")]
        public async Task<IActionResult> GetPagedProducts(int? pageNumber)
        {
            try
            {
                _logger.LogDebug("{MethodName}", nameof(GetPagedProducts));
                var pagedProducts = await _productService.GetPagedProducts(pageNumber);
                return Ok(new { message = "Get paged products", products = pagedProducts });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred getting paged products");
                return StatusCode(500, new { message = "Unexpected error updating product", error = ex.Message });
            }
        }
    }
}   
