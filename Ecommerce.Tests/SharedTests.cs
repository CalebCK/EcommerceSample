using Ecommerce.Shared.Extensions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Tests
{
    public class SharedTests
    {
        public SharedTests()
        {

        }

        [Theory]
        [InlineData("My big brother shared his cake with me.", new string[] { "my", "big", "brother", "shared", "his", "cake", "with", "me" })]
        [InlineData("It is growing cold.", new string[] { "it", "is", "growing", "cold" })]
        [InlineData("Tom, John and Mary shared an umbrella.", new string[] { "tom", "john", "and", "mary", "shared", "an", "umbrella" })]
        [InlineData("I want to see the film again.", new string[] {"i", "want", "to", "see", "the", "film", "again" })]
        [InlineData("Stand up and catch that ball!", new string[] { "stand", "up", "and", "catch", "that", "ball" })]
        public void GetSentenceWords(string sentence, string[] words)
        {
            //act
            var result = GlobalFunctions.GetSentenceWords(sentence).ToArray();

            //Assert
            Assert.Equal(result, words);
        }

        [Fact]
        public async Task ApiRequesterTestAsync()
        {
            //arrange
            string _testUrl = "https://httpbin.org/anything";
            var parameters = TestDataGenerator.Params;
            var apiModel = new ApiModel() { Url = _testUrl, RequestMethod = Method.GET, Parameters = parameters };

            //act
            var response = await ApiRequestor.GetAsync(apiModel);

            //assert
            Assert.True(response.IsSuccessful);
        }
    }
}
