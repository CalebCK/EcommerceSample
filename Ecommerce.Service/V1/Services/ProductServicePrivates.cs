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
    public partial class ProductService
    {
        private void FilterByMaxPrice(int? maxPrice, ref IEnumerable<Product> products)
        {
            if(maxPrice is null)
                return;

            products = products.Where(x => x.Price <= maxPrice).ToList();
        }

        private void FilterBySize(string size, ref IEnumerable<Product> products)
        {
            if (string.IsNullOrEmpty(size))
                return;

            products = products.Where(x => x.Sizes.Contains(size)).ToList();
        }

        private void FilterByHighlight(string highlight, ref IEnumerable<Product> products)
        {
            if (string.IsNullOrEmpty(highlight))
                return;

            var words = GlobalFunctions.GetSentenceWords(highlight);

            List<Product> filteredProducts = new List<Product>();
            foreach (var product in products)
            {
                //check if description constains any of the words in highlight
                var isCandidate = words.Any(product.Description.ToLower().Contains);

                if (isCandidate)
                {
                    product.Description = HighlightDescription(words.ToArray(), product.Description);

                    filteredProducts.Add(product);
                }

            }

            products = filteredProducts;
        }

        private string HighlightDescription(string[] highlightWords, string productDescription)
        {
            string newDescription = productDescription;

            foreach (var word in highlightWords)
            {
                if(productDescription.Contains(word))
                    newDescription = newDescription.Replace(word, $"<em>{word}</em>");
            }

            return newDescription;
        }

        private ProductFilterDto GetProductFilterObject(List<Product> products)
        {
            //var products = await _productRepository.GetAllAsync();

            ProductFilterDto filter = new();

            filter.MinPrice = products.Min(x => x.Price);
            filter.MaxPrice = products.Max(x => x.Price);

            List<string> sizes = new();
            products.ToList().ForEach(p => { sizes.AddRange(p.Sizes.ToList()); });
            filter.AvailableSizes = sizes.Distinct().ToArray();

            List<List<string>> productSplitDescriptions = new List<List<string>>();
            products.ToList().ForEach(d => { productSplitDescriptions.Add(GlobalFunctions.GetSentenceWords(d.Description).ToList()); });

            var uniqueListItems = productSplitDescriptions.SelectMany(l => l)
                .GroupBy(l => l)
                .Select(l => new { Key = l.Key, Count = l.Count() })
                .OrderByDescending(o => o.Count);

            filter.CommonWords = uniqueListItems.Skip(AppConstants.CommonWordsSkipSize).Take(AppConstants.CommonWordsTakeSize).Select(x=>x.Key).ToArray();

            return filter;
        }
    }
}
