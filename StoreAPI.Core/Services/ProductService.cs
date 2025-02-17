using AutoMapper;
using StoreAPI.Common.Dtos;
using StoreAPI.Core.Interfaces;
using StoreAPI.Infraestructure.EntityFramework.Daos;
using StoreAPI.Infraestructure.Interfaces;
using StoreAPI.Infraestructure.EntityFramework.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace StoreAPI.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<ProductService> logger
            ) 
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductHomeDto>> GetFeaturedProductsAsync()
        {
            var featuredProducts = await _productRepository.GetAllFeaturedAsync();
            return _mapper.Map<IEnumerable<ProductHomeDto>>(featuredProducts);
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            var existentProduct = await _productRepository.GetProduct(productId);
            return existentProduct;
        }

        public async Task DeleteProductAsync(int productId) {
            var productToDelete = await _productRepository.GetByIdAsync(productId);
            if (productToDelete is null)
            {
                throw new NullReferenceException("Product to delete not found");
            }
            _productRepository.Delete(productToDelete);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ProductDetailDto> GetProductDetailAsync(int id)
        {
            var product = await _productRepository.GetProduct(id); 
            return _mapper.Map<ProductDetailDto>(product);
        }

        public async Task<IEnumerable<ProductHomeDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map < IEnumerable<ProductHomeDto>>(products);
        }

        public async Task<PagedResponseKeyset<ProductResultDto>> GetWithKeysetPagination(int reference, int pageSize)
        {
            var pagedProducts = await _productRepository.GetWithKeysetPagination(reference, pageSize);
            return _mapper.Map<PagedResponseKeyset<ProductResultDto>>(pagedProducts);
        }

        public async Task<CreateProductDto> Post(CreateProductDto model)
        {
            try
            {
                var newProduct = _mapper.Map<Product>(model);
                newProduct.Guid = Guid.NewGuid(); // Genera el GUID

                await _productRepository.AddAsync(newProduct);

                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<CreateProductDto>(newProduct);
            }
            catch (Exception e)
            {
                _logger.LogError("Error posting product: " + e.Message);
                throw new Exception("Error while posting product", e);
            }
        }

        public async Task<CreateProductDto> Put(int productId, CreateProductDto model)
        {
            var existingProduct = await _productRepository.GetByIdAsync(productId);

            if (existingProduct is null)
            {
                throw new Exception("Product not found");
            }

            _mapper.Map(model, existingProduct);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CreateProductDto>(existingProduct);
        }

    }
}
