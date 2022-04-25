using AutoMapper;
using Ecommerce.Api.V1.DataTransferObjects;
using Ecommerce.Domain.V1.Services.IServices;
using Ecommerce.Repository.Models.Data;
using Ecommerce.Repository.Repositories.IRepositories;
using Ecommerce.Shared.Constants;
using Ecommerce.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.V1.Services
{
    public partial class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponseDto> GetProducts()
        {
            ProductResponseDto responseDto = new();
            var products = await _productRepository.GetAllAsync();

            responseDto.Products = _mapper.Map<List<ProductDto>>(products);
            responseDto.FilterObject = GetProductFilterObject(products.ToList());

            return responseDto;
        }

        public async Task<ProductResponseDto> GetProducts(ProductQueryDto productQueryDto)
        {
            ProductResponseDto responseDto = new();
            var products = await _productRepository.GetAllAsync();
            
            FilterByMaxPrice(productQueryDto.MaxPrice, ref products);

            FilterBySize(productQueryDto.Size, ref products);

            FilterByHighlight(productQueryDto.Highlight, ref products);

            responseDto.Products = _mapper.Map<List<ProductDto>>(products);
            responseDto.FilterObject = GetProductFilterObject(products.ToList());

            return responseDto;
        }
    }
}
