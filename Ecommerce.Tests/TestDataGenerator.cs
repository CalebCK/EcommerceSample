using Ecommerce.Api.V1.DataTransferObjects;
using Ecommerce.Shared.Extensions;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Tests
{
    internal static class TestDataGenerator
    {
        public static readonly List<KeyValuePair<string, string>> Params = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("param1", "firstParameter"),
            new KeyValuePair<string, string>("param2", "secondParameter"),
            new KeyValuePair<string, string>("param3", "thirdParameter")
        };
    }
}
