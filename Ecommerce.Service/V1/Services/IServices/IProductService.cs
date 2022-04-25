using Ecommerce.Api.V1.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.V1.Services.IServices
{
    public interface IProductService
    {
        Task<ProductResponseDto> GetProducts();
        Task<ProductResponseDto> GetProducts(ProductQueryDto productQueryDto);
    }
}
