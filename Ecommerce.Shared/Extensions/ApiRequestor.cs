using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Extensions
{
    public class ApiModel
    {
        public string Url { get; set; }
        public Method RequestMethod { get; set; }
        public List<KeyValuePair<string, string>> Parameters { get; set; }
    }

    public static class ApiRequestor
    {
        public static async Task<IRestResponse> GetAsync(ApiModel model)
        {
            RestClient restClient = new RestClient(model.Url);

            var request = new RestRequest(model.RequestMethod);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");

            //loop through assign parameters
            model.Parameters?.ForEach(p => { request.AddParameter(p.Key, p.Value); });

            IRestResponse restResponse = await restClient.ExecuteAsync(request);

            return restResponse;
        }
    }
}
