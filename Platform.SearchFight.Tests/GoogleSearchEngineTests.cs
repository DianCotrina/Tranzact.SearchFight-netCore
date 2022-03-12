using Microsoft.Extensions.Options;
using Moq;
using Platform.SearchFight.Service.Engine;
using Platform.SearchFight.Service.Engine.Config;
using Platform.SearchFight.Service.Engine.Interfaces;
using Platform.SearchFight.Service.Engine.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Platform.SearchFight.Tests
{
    public class GoogleSearchEngineTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFacotry = new Mock<IHttpClientFactory>();
        private readonly Mock<IOptions<SearchGoogleConfig>> _optionsMock = new Mock<IOptions<SearchGoogleConfig>>();

        private GoogleSearchEngine Subject => new GoogleSearchEngine(_optionsMock.Object,_httpClientFacotry.Object);

        [Fact]
        public async Task GOOGLE_It_ShouldReturnSearchWinner_Success() 
        {
            _httpClientFacotry.Setup(hcf => hcf.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient());

            var options = new SearchGoogleConfig
            {
                ApiKey = "", //USE YOUR KEY HERE
                BaseUrl = "https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}",
                ContextId = "018022716490518442170:uctnoratmy8",
                EngineName = "Google"
            };

            _optionsMock.Setup(opt => opt.Value).Returns(options);

            var topic = "java";

            var result = await Subject.GetSearchResultCount(topic);

            Assert.NotNull(result);
            Assert.IsType<SearchWinner>(result);
        }

        [Fact]
        public async Task GOOGLE_It_ShouldReturnSearchWinner_Fail()
        {
            _httpClientFacotry.Setup(hcf => hcf.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient());

            var options = new SearchGoogleConfig
            {
                ApiKey = "", //USE YOUR KEY HERE
                BaseUrl = "https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}",
                ContextId = "018022716490518442170:uctnoratmy8",
                EngineName = "Google"
            };

            _optionsMock.Setup(opt => opt.Value).Returns(options);

            Func<Task> act = () => Subject.GetSearchResultCount(string.Empty);

            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }
    }

    /*  "GoogleSearchEngine": {
    "EngineName": "Google",
    "ContextId": "018022716490518442170:uctnoratmy8",
    "ApiKey": "",
    "BaseUrl": "https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}"
    },*/
}
