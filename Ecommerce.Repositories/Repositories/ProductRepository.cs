using Ecommerce.Repository.Models.Data;
using Ecommerce.Repository.Repositories.IRepositories;
using Ecommerce.Shared.Extensions;
using Ecommerce.Shared.Extensions.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private string _productConnectionUrl;

        public ProductRepository(IConfiguration configuration)
        {
            _productConnectionUrl = configuration.GetConnectionString("ProductConnectionUrl");
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            ApiModel apiModel = new()
            {
                Url = _productConnectionUrl,
                RequestMethod = RestSharp.Method.GET
            };

            var response = await ApiRequestor.GetAsync(apiModel);

            if (response.IsSuccessful)
            {
                ProductResponse products = JsonConvert.DeserializeObject<ProductResponse>(response.Content);

                //log content to file
                FileStreamLogger.Log(JsonConvert.SerializeObject(response.Content));

                return products.Products;
            }

            throw new CustomException(JsonConvert.SerializeObject(response.Content));
        }
    }
}
