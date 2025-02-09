using AutoMapper;
using StoreAPI.Common.Dtos;
using StoreAPI.Core.Interfaces;
using StoreAPI.Infraestructure.EntityFramework.Daos;
using StoreAPI.Infraestructure.Interfaces;
using StoreAPI.Infraestructure.EntityFramework.UnitOfWork;

namespace StoreAPI.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(
            IProductRepository productRepository,
            IMapper mapper
            ) 
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<PagedResponseKeyset<ProductResultDto>> GetWithKeysetPagination(int reference, int pageSize)
        {
            var pagedProducts = await _productRepository.GetWithKeysetPagination(reference, pageSize);
            return _mapper.Map<PagedResponseKeyset<ProductResultDto>>(pagedProducts);
        }

        public async Task<ProductDto> GetDto(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDto> Post(int productId, ProductDto model)
        {
            throw new NotImplementedException();
        }
    }
}
