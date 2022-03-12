using Microsoft.Extensions.Options;
using Moq;
using Platform.SearchFight.Service.Engine;
using Platform.SearchFight.Service.Engine.Config;
using Platform.SearchFight.Service.Engine.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Platform.SearchFight.Tests
{
    public class BingSearchEngineTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFacotry = new Mock<IHttpClientFactory>();
        private readonly Mock<IOptions<SearchBingConfig>> _optionsMock = new Mock<IOptions<SearchBingConfig>>();

        private BingSearchEngine Subject => new BingSearchEngine(_optionsMock.Object, _httpClientFacotry.Object);

        [Fact]
        public async Task BING_It_ShouldReturnSearchWinner_Success()
        {
            _httpClientFacotry.Setup(hcf => hcf.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient());

            var options = new SearchBingConfig
            {
                ApiKey = "", //USE YOUR KEY HERE
                BaseUrl = "https://api.bing.microsoft.com/v7.0/search?q=",
                EngineName = "Bing"
            };

            _optionsMock.Setup(opt => opt.Value).Returns(options);

            var topic = "java";

            var result = await Subject.GetSearchResultCount(topic);

            Assert.NotNull(result);
            Assert.IsType<SearchWinner>(result);
        }

        [Fact]
        public async Task BING_It_ShouldReturnSearchWinner_Fail()
        {
            _httpClientFacotry.Setup(hcf => hcf.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient());

            var options = new SearchBingConfig
            {
                ApiKey = "", //USE YOUR KEY HERE
                BaseUrl = "https://api.bing.microsoft.com/v7.0/search?q=",
                EngineName = "Bing"
            };

            _optionsMock.Setup(opt => opt.Value).Returns(options);

            Func<Task> act = () => Subject.GetSearchResultCount(string.Empty);

            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }
    }

    /*"BingSearchEngine": {
    "EngineName": "Bing",
    "ApiKey": "",
    "BaseUrl": "https://api.bing.microsoft.com/v7.0/search?q="
    }*/
}
