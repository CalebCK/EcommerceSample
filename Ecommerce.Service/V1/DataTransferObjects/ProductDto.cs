using Ecommerce.Shared.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ecommerce.Api.V1.DataTransferObjects
{
    public class ProductResponseDto
    {
        public ProductFilterDto FilterObject { get; set; }
        public List<ProductDto> Products { get; set; }
    }

    public class ProductFilterDto
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string[] AvailableSizes { get; set; }
        public string[] CommonWords { get; set; }
    }

    public class ProductDto
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string[] Sizes { get; set; }
        public string Description { get; set; }
    }

    public class ProductQueryDto : IValidatableObject
    {
        public int? MaxPrice { get; set; }
        public string Size { get; set; }
        public string Highlight { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MaxPrice is not null && MaxPrice < 1)
            {
                yield return new ValidationResult("Max price should be more than 0", new string[] { "MaxPrice" });
            }
            if (!string.IsNullOrEmpty(Size))
            {
                var sizeValid = AppConstants.ProductSizes.Any(x => x.ToLower().Trim() == Size.ToLower().Trim());

                if(!sizeValid)
                    yield return new ValidationResult("Provide a valid size (small, medium or large)", new string[] { "Size" });
            }
        }
    }
}
