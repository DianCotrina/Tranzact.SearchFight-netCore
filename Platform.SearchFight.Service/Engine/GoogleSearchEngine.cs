using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Platform.SearchFight.Service.Engine.Config;
using Platform.SearchFight.Service.Engine.Interfaces;
using Platform.SearchFight.Service.Engine.Model;
using Platform.SearchFight.Service.Engine.Model.Response;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Platform.SearchFight.Service.Engine
{
    public class GoogleSearchEngine : ISearchEngine
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SearchGoogleConfig _configuration;

        public GoogleSearchEngine(IOptions<SearchGoogleConfig> configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration.Value;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<SearchWinner> GetSearchResultCount(string topic)
        {
            try
            {
                if(string.IsNullOrEmpty(topic))
                    throw new ArgumentNullException(nameof(topic), "Topic is invalid. Please select a valid topic");

                string apiKey = _configuration.ApiKey;
                string CONTEXT_ID = _configuration.ContextId;
                string baseUrl = _configuration.BaseUrl;

                var client = _httpClientFactory.CreateClient();

                var response = await client.GetAsync(string.Format(baseUrl, apiKey, CONTEXT_ID, topic));

                var searchInformation = JsonConvert.DeserializeObject<GoogleResponse>(await response.Content.ReadAsStringAsync());

                if (searchInformation != null)
                {
                    return new SearchWinner()
                    {
                        EngineName = _configuration.EngineName,
                        Topic = topic,
                        Results = long.Parse(searchInformation.SearchInformation.TotalResults)
                    };
                }

                throw new ArgumentException("The current process has failed. No data encountered");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
