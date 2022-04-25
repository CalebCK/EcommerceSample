using Ecommerce.Api.V1.DataTransferObjects;
using Ecommerce.Domain.V1.Services.IServices;
using Ecommerce.Shared.Extensions;
using Ecommerce.Shared.Extensions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _productService.GetProducts();

            if (data == null || data.Products.Count == 0)
                return NoContent();

            return Ok(data);
        }

        /// <summary>
        /// Apply filter to products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> FilterAsync(int? maxPrice, string size, string highlight)
        {
            ProductQueryDto productQueryDto = new() { MaxPrice = maxPrice, Size = size, Highlight = highlight };
            var validationResult = GlobalFunctions.IsModelValid(productQueryDto);

            if (!validationResult.Key)
                throw new CustomException(GlobalFunctions.GetModelValidationErrors(validationResult.Value));

            var data = await _productService.GetProducts(productQueryDto);

            if (data is null || data.Products.Count == 0)
                return NoContent();

            return Ok(data);
        }
    }
}
